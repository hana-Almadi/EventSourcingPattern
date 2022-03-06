

using account_event_sourcing.Controllers.Dto;
using account_event_sourcing.Domain;
using account_event_sourcing.Domain.Event;
using account_event_sourcing.Repository;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace account_event_sourcing
{
    public class AccountService
    {
        private readonly Repository<Account, Guid> _accountRepository;
        private readonly Repository<AccountAggergate,Guid> _accountAggergateRepository;
        private readonly Service.EventHandler<DomainEvent> _eventHandler;
        private readonly IMapper _mapper;

        public AccountService(Repository<Account, Guid> repository, 
            Repository<AccountAggergate, Guid> accountAggergateRepository,
            Service.EventHandler<DomainEvent> eventHandler, IMapper mapper)
        {
            _accountRepository = repository;
            _accountAggergateRepository = accountAggergateRepository;
            _eventHandler = eventHandler;
            _mapper = mapper;
        }
        public async Task<Guid> CreateAccount(CreateAccountDto createAccountDto)
        {
            var accountAggergate = new AccountAggergate(createAccountDto.AccountOwnerName, 
                createAccountDto.AccountOwnerId,createAccountDto.AccountOwnerEmail,
                createAccountDto.AccountOwnerPhone, Guid.NewGuid());
            accountAggergate.AccountList.Add(new Account());
            await _accountAggergateRepository.Save(accountAggergate);
            await _eventHandler.SaveEvent(new AccountCreateEvent(accountAggergate.OwnerName,
                accountAggergate.AccountNumber, accountAggergate.OwnerID,
                accountAggergate.OwnerEmail, accountAggergate.OwnerPhone, accountAggergate.Id));
            return accountAggergate.AccountNumber;
        }
        public async Task Deposit(DepositDto depositDto)
        {
            var accountAggergate = await _accountAggergateRepository.FindById(depositDto.AccountNumber);
            var account = accountAggergate.AccountList.LastOrDefault();
            var depositAccount = new Account(account.Money,account.AccountAggergateId);
            depositAccount.DepositMoney(depositDto.Money);
            accountAggergate.AddAccount(depositAccount);
            await _accountAggergateRepository.Update(accountAggergate);
            await _eventHandler.SaveEvent(new AccountDepositEvent(depositDto.Money, 
                new Account(account.Money,account.AccountAggergateId), accountAggergate.Id));

        }

        public async Task Transfer(TransferDto transferDto)
        {
            var accountAggergateFrom = await _accountAggergateRepository.FindById(transferDto.FromAccountNumber);
            var accountFrom = accountAggergateFrom.AccountList.LastOrDefault();
            var transferAccount = new Account(accountFrom.Money, accountFrom.AccountAggergateId);

            var accountAggergateTo = await _accountAggergateRepository.FindById(transferDto.ToAccountNumber);
            var accountTo = accountAggergateTo.AccountList.LastOrDefault();
            var depositAccount = new Account(accountTo.Money, accountTo.AccountAggergateId);

            transferAccount.WithdrawtMoney(transferDto.Money);
            depositAccount.DepositMoney(transferDto.Money);
            accountAggergateFrom.AddAccount(transferAccount);
            accountAggergateTo.AddAccount(depositAccount);
            await _accountAggergateRepository.Update(accountAggergateFrom);
            await _accountAggergateRepository.Update(accountAggergateTo);
            await _eventHandler.SaveEvent(new AccountTransferEvent(transferDto.Money, 
                new Account(accountFrom.Money, accountFrom.AccountAggergateId), accountAggergateFrom.Id));
            await _eventHandler.SaveEvent(new AccountDepositEvent(transferDto.Money,
               new Account(accountTo.Money, accountTo.AccountAggergateId), accountAggergateTo.Id));
        }

        public async Task<List<TransactionDto>> Transaction(Guid accountNumber)
        {
            ICollection<Account> accounts = (await _accountAggergateRepository.FindById(accountNumber)).AccountList;
            var transactionDtos=_mapper.Map<ICollection<Account>, ICollection<TransactionDto>>(accounts);
            return transactionDtos.ToList();
        }

    }
}
