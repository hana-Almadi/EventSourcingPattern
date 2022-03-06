using account_event_sourcing.Controllers.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace account_event_sourcing.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountController:ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(AccountService accountService, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        [HttpPost("/create")]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountDto createAccountDto)
        {
            _logger.LogInformation("receive request for create account with payload:\n"+ createAccountDto.ToString());
            var accountNumber = await _accountService.CreateAccount(createAccountDto);
            _logger.LogInformation("create account successfully with account number:" + accountNumber);
            return Ok("Your account was createing successfully , your account number: " + accountNumber);
        }

        [HttpPost("/deposit")]
        public async Task<IActionResult> Deposit([FromBody] DepositDto depositDto)
        {
            _logger.LogInformation("receive request for deposit account with payload: \n" + depositDto.ToString());
            await _accountService.Deposit(depositDto);
            _logger.LogInformation("Your account has been deposited successfully");
            return Ok("Your account has been deposited successfully");
        }

        [HttpPost("/transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferDto transferDto)
        {
            _logger.LogInformation("receive request for transfer account with payload: \n" + transferDto.ToString());
            await _accountService.Transfer(transferDto);
            _logger.LogInformation("transfer is done successfully");
            return Ok("The transfer is done successfully");
        }

        [HttpGet("/transactions/{accountNumber}")]
        public async Task<IActionResult> Transaction(Guid accountNumber)
        {
            _logger.LogInformation("receive request for get transaction of account by accountNumber: " + accountNumber);
            var transactionDto = await _accountService.Transaction(accountNumber);
            _logger.LogInformation("account transaction \n" + transactionDto.ToString());
            return Ok( transactionDto);

        }

    }
}
