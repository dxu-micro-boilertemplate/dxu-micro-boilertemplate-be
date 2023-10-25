using System.Linq.Expressions;
using API.Application.Models.ViewModels;
using API.Data.Repositories;
using AutoMapper;
using H.Core.Helpers.Paging;
using H.Core.Helpers.Paging.Object;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Queries;

public class GetUsersQuery : PagingParam<UserSortCriteria>, IRequest<PagingResponseQuery<UserResponse, UserSortCriteria>>
{
    
}

public enum UserSortCriteria
{
    Id,
    Name
}

public class GetUsersQueryHandler :  IRequestHandler<GetUsersQuery, PagingResponseQuery<UserResponse, UserSortCriteria>>
{
    private IMapper _mapper;
    private IUserRepo _repo;

    public GetUsersQueryHandler(IMapper mapper, IUserRepo repo)
    {
        _mapper = mapper;
        _repo = repo;
    }

    public async Task<PagingResponseQuery<UserResponse, UserSortCriteria>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var query = _repo.GetUsers(null, new Expression<Func<Data.Models.User, object>>[]
        {
        });
        var total = await query.CountAsync(cancellationToken: cancellationToken);
        
        query = query.GetWithSorting(request.SortKey.ToString(), request.SortOrder);
        
        query = query.GetWithPaging(request.Page, request.PageSize);

        var result = this._mapper.ProjectTo<UserResponse>(query);

        return new PagingResponseQuery<UserResponse, UserSortCriteria>(request, result, total);
    }
}