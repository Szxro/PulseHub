import { SuccessResponse } from "./success-response.model";
import { ErrorResponse } from "./error-response.model";

type SuccesOutCome<T> = { success: boolean, data: SuccessResponse<T> };

type FailureOutCome = { success: boolean, reason: ErrorResponse };

type Outcome<T> = SuccesOutCome<T> | FailureOutCome;

export type { Outcome } 