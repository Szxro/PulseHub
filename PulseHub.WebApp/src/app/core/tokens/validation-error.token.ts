import { InjectionToken } from "@angular/core";
import { DEFAULT_VALIDATION_MESSAGES } from "../constants/default-validation-messages.constant";
import { ErrorHandler } from "../models/handlers/error-handler.model";

export const VALIDATION_ERROR_TOKEN = new InjectionToken<ErrorHandler>('Validation Errors Token',{
    providedIn:'root',
    factory: () => DEFAULT_VALIDATION_MESSAGES
});