namespace TiaSoftBackend.UseCases.ErrorCodes;

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
    
    // MENU ERRORS
    MenuNotFound,
    MenuNotUpdated,
    MenuNotCreated,
    MenuImageCannotBeUploaded,
    
    // TABLE STATUS ERRORS
    TableStatusNotFound,
}