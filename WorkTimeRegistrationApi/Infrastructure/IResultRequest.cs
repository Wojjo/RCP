using MediatR;
using WorkTimeRegistrationShared.Infrastructure;

namespace WorkTimeRegistrationApi.Infrastructure;

public interface IResultRequest<TResponse> : IRequest<Result<TResponse>> where TResponse : class { }

public interface IResultRequest : IRequest<Result> { }

public interface IResultRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, Result<TResponse>> where TRequest : IResultRequest<TResponse> where TResponse : class
{ }

public interface IResultRequestHandler<TRequest> : IRequestHandler<TRequest, Result> where TRequest : IResultRequest
{ }
