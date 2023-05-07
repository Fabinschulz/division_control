import { HttpClient } from "@angular/common/http";
import { Observable, catchError, map, throwError } from "rxjs";
import { IServiceModelResponse } from "../../interfaces/response";
import { Divida } from "../models/divida";
import { environment } from "src/enviroments/environment";
import { parseModel } from "src/app/shared/constants/util";
import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})

export class DividasService {
  constructor(private httpClient: HttpClient) {}

  registrarDivida(formData: any): Observable<IServiceModelResponse<Divida>> {
    return this.httpClient.post(`${environment.apiUrl}Divida/registrar`, formData).pipe(
      map((response: any): IServiceModelResponse<Divida> => {
        return {
          ...response,
          model: parseModel(response.model),
        };
      })
    );
  }

  atualizarDivida(formData: any): Observable<IServiceModelResponse<Divida>> {
    return this.httpClient.put(`${environment.apiUrl}Divida/atualizar`, formData).pipe(
      map((response: any): IServiceModelResponse<Divida> => {
        return {
          ...response,
          model: parseModel(response.model),
        };
      })
    );
  }

  removerDivida(id: string): Observable<any> {
    return this.httpClient.delete(`${environment.apiUrl}Divida/remover/${id}`);
  }

  obterDividas(): Observable<any> {
    return this.httpClient
    .get(`${environment.apiUrl}Divida/obter-listagem`)
    .pipe(
      map((response: any): IServiceModelResponse<Divida[]> => response.model),
      catchError(error => {
        return throwError(() => error);
      })
    );
  }

  obterPorId(id: string): Observable<Divida> {
    return this.httpClient
    .get(`${environment.apiUrl}Divida/obter/${id}`)
    .pipe(
      map((response: any): Divida => response.model),
      catchError(error => {
        return throwError(() => error);
      })
    );
  }
}
