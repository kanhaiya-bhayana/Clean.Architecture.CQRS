using Clean.Architecture.Core.Accounts.Commands.Create;
using Clean.Architecture.Core.Accounts.Commands.Delete;
using Clean.Architecture.Core.Accounts.Commands.Update;
using Clean.Architecture.Core.Accounts.Queries.Get;
using Clean.Architecture.Core.Accounts.Queries.GetAll;
using Clean.Architecture.Core.Common.Request;
using Clean.Architecture.Core.Common.Response;
using Clean.Architecture.Core.Common.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Clean.Architecture.API.Controllers
{
    [Route("api/[controller]")]
    //[TypeFilter(typeof(AuthorizationFilterAttribute))]
    [ApiController]
    public class AccountController : ControllerBase
    {
        //private readonly IAccountService _accountService;
        //private readonly ICommandHandler<CreateAccountCommand> _createAccountHandler;
        //private readonly ICommandHandler<UpdateAccountCommand> _updateAccountHandler;
        //private readonly ICommandHandler<DeleteAccountCommand> _deleteAccountHandler;
        //private readonly IQueryHandler<GetAllAccountQuery, IEnumerable<AccountResponse>> _getAllAccountHandler;
        //private readonly IQueryHandler<GetAccountByNumberQuery, AccountResponse> _getAccountByNumberHandler;
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getaccounts")]
        public async Task<ApiResponse> GetAll()
        {
            var apiResponse = new ApiResponse();

            try
            {
                var data = await _mediator.Send(new GetAllAccountQuery());
                apiResponse.Success = true;
                apiResponse.Result = data;
            }
            catch (SqlException ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
            }

            return apiResponse;
        }

        [HttpGet("getaccount/")]
        [Authorize]
        public async Task<ApiResponse> GetByAccountNumber(long accountNumber)
        {
            var apiResponse = new ApiResponse();
            var query = new GetAccountByNumberQuery { AccountNumber = accountNumber };
            try
            {
                var data = await _mediator.Send(query);
                apiResponse.Success = true;
                apiResponse.Result = data;
            }
            catch (SqlException ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
            }

            return apiResponse;
        }



        [HttpPost("createaccount")]
        public async Task<ApiResponse> CreateAccount(AccountRequest request)
        {
            var apiResponse = new ApiResponse();

            try
            {
                await _mediator.Send(new CreateAccountCommand { AccountRequest = request });
                apiResponse.Success = true;
                apiResponse.Message = AccountConstants.MESSAGE_201;
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = AccountConstants.MESSAGE_500;
                apiResponse.Result = ex.Message;
            }

            return apiResponse;
        }

        [HttpPut("updateaccount")]
        public async Task<ApiResponse> UpdateAccount(AccountRequest account)
        {
            var apiResponse = new ApiResponse();
            var command = new UpdateAccountCommand { AccountRequest = account };
            try
            {
                await _mediator.Send(command);
                apiResponse.Success = true;
                apiResponse.Message = AccountConstants.MESSAGE_200 ;
            }
            catch (SqlException ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
            }

            return apiResponse;
        }

        [HttpDelete("deleteaccount/{accountNumber}")]
        public async Task<ApiResponse> DeleteAccount(long accountNumber)
        {
            var apiResponse = new ApiResponse();

            try
            {
                await _mediator.Send(new DeleteAccountCommand { AccountNumber = accountNumber });
                apiResponse.Success = true;
                apiResponse.Message = AccountConstants.MESSAGE_200;
            }
            catch (SqlException ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
            }

            return apiResponse;
        }
    }
}
