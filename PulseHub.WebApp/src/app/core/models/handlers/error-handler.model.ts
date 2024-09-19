type ErrorRegistry = { [key in string]: (...args:any) => string };

interface ErrorHandler{
    global?:ErrorRegistry,
    field?:ErrorRegistry
}

export type { ErrorHandler }; 