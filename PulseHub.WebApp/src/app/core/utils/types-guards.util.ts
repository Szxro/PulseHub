import { ErrorResponse } from "../models/responses/error-response.model";

export function isErrorResponse(error:unknown):error is ErrorResponse{
    return error != null && (error as ErrorResponse).detail !== undefined;
}