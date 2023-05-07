export interface IServiceModelResponse<T> {
  code: string;
  success: boolean;
  model?: T;
  errors: boolean;
}
