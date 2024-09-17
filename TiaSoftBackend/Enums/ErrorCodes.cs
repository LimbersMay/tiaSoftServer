namespace TiaSoftBackend.Enums;

public enum ErrorCodes
{
    // AUTH ERRORS
    AuthErrorIncorrectCredentials,
    AuthErrorEmailAlreadyExists,
    AuthErrorNotAuthorized,
    
    // USER ERRORS
    UserNotFound,
    UserNotUpdated,
    UserErrorWhenUpdatingUSer,
    UserErrorWhenCreatingUser,
    UserErrorUserNotCreated,
    
    // TABLE ERRORS
    TableNotFound,
}