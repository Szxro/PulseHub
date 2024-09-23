import { ToastMessage } from "../models/others/toast-message.model";
import { ErrorResponse } from "../models/responses/error-response.model";

export function isErrorResponse(error:unknown):error is ErrorResponse{
    return error != null && (error as ErrorResponse).detail !== undefined;
}

export function isToastMessage(error:unknown):error is ToastMessage{
    return error != null && (error as ToastMessage).message !== undefined;
}