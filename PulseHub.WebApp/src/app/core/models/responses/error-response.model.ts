export interface ErrorResponse{
    type:    string;
    title:   string;
    status:  number;
    detail:  string;
    traceId: string;
    errors?: Record<string,Errors[]>
}

interface Errors{
    code: string;
    message: string;
}