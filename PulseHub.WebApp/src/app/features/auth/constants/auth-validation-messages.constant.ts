import { DEFAULT_VALIDATION_MESSAGES } from "../../../core/constants/default-validation-messages.constant";
import { ErrorHandler } from "../../../core/models/handlers/error-handler.model";

export const AUTH_VALIDATION_MESSAGES: ErrorHandler = {
    global:{
        ...DEFAULT_VALIDATION_MESSAGES.global,
        passwordMatchError: () => "Passwords must match.",
    },
    field:{
        ...DEFAULT_VALIDATION_MESSAGES.field,
        weakPassword: () => "The password at least need to have one uppercase, lowercase, number and special character",
        invalidUsername: () => "The username can only contain letters, numbers, and underscores",
        email:() => "The provide email need to be a valid email"
    }
}