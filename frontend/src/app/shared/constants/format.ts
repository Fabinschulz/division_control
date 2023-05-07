import { CurrencyPipe } from "@angular/common";

export function formatPercentage(value: number): string {
  return value.toFixed(2) + '%';
}

export function formatCPF(value: string): string {
  return value.replace(/^(\d{3})(\d{3})(\d{3})(\d{2})$/, '$1.$2.$3-$4');
}

export function formatCurrency(value: number): any {
  const currencyPipe = new CurrencyPipe('pt-BR');

  return currencyPipe.transform(value, 'BRL', 'symbol', '1.2-2');
}

export const MASK_TYPES = {
  CNPJ: '00.000.000/0000-00',
  PHONE: '(00) 0000-0000||(00) 00000-0009',
  CPF: '000.000.000-00',
  CPF_CNPJ: '000.000.000-00||00.000.000/0000-00',
  DATE: 'd0/M0/0000',
  CEP: '00000-000',
  PERCENT: 'separator.3',
};



