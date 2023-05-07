import { Parcela } from "./parcela";

export class Divida{
  id: string;
  numeroDoTitulo: string;
  nomeDoDevedor: string;
  cpfDoDevedor: string;
  juros: number;
  multa: number;
  quantidadeDeParcelas: number;
  valorAtualizado: number;
  valorOriginal: number;
  diasEmAtraso: number;
  parcelas: Parcela[];
}
