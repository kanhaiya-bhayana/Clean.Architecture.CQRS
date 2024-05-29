using Clean.Architecture.Core.Common.Mapper;
using Clean.Architecture.Core.Interfaces;
using MediatR;

namespace Clean.Architecture.Core.Accounts.Commands.Update
{
    public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAccountCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateAccountCommand command, CancellationToken cancellationToken)
        {
            var account = AccountMapper.MapToAccount(command.AccountRequest);
            await _unitOfWork.AccountRepository.UpdateAsync(account);
        }
    }
}
