namespace TiaSoftBackend.UseCases.ErrorCodes;

public static class ErrorCodes
{
    // AUTH ERRORS
    public const string AuthErrorIncorrectCredentials = "AuthErrorIncorrectCredentials";
    public const string AuthErrorEmailAlreadyExists = "AuthErrorEmailAlreadyExists";
    public const string AuthErrorNotAuthorized = "AuthErrorNotAuthorized";
    
    // USER ERRORS
    public const string UserNotFound = "UserNotFound";
    public const string UserNotUpdated = "UserNotUpdated";
    public const string UserErrorWhenUpdatingUSer = "UserErrorWhenUpdatingUSer";
    public const string UserErrorWhenCreatingUser = "UserErrorWhenCreatingUser";
    public const string UserErrorUserNotCreated = "UserErrorUserNotCreated";
    
    // TABLE ERRORS
    public const string TableNotFound = "TableNotFound";
    public const string TableNotUpdated = "TableNotUpdated";
    public const string TableNotCreated = "TableNotCreated";
    
    // MENU ERRORS
    public const string MenuNotFound = "MenuNotFound";
    public const string MenuNotUpdated = "MenuNotUpdated";
    public const string MenuNotCreated = "MenuNotCreated";
    public const string MenuImageCannotBeUploaded = "MenuImageCannotBeUploaded";
    
    // TABLE STATUS ERRORS
    public const string TableStatusNotFound = "TableStatusNotFound";
    public const string TableStatusNotUpdated = "TableStatusNotUpdated";
    public const string TableStatusNotCreated = "TableStatusNotCreated";
    public const string TableStatusNotDeleted = "TableStatusNotDeleted";
    
    // AREA ERRORS
    public const string AreaNotFound = "AreaNotFound";
    public const string AreaNotUpdated = "AreaNotUpdated";
    public const string AreaNotCreated = "AreaNotCreated";
    
} 