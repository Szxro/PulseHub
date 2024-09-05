export interface SuccessResponse<T = unknown>{
    detail: string;
    type:   string;
    status: number;
    data: T
}