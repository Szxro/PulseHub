export interface LoginRequest{
    username:string;
    password:string;
}

export interface RegisterRequest{
    firstname:string;
    lastname:string;
    username:string;
    email:string;
    password:string;
}

export interface RegenerateTokenRequest{
    token:string;
    refreshToken:string;
}