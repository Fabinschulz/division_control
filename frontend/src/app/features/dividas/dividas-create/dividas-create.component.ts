import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { DividasService } from "../services/dividas.service";
import { Divida } from "../models/divida";
import { take } from "rxjs";
import { ToastService } from "../services/toast.service";
import { MASK_TYPES } from "src/app/shared/constants/format";
import { Router } from "@angular/router";

@Component({
  selector: 'app-dividas-create',
  templateUrl: './dividas-create.component.html',
  styleUrls: ['./dividas-create.component.scss'],
})
export class DividasCreateComponent implements OnInit {
  public formGroup: FormGroup;
  public MASK = MASK_TYPES;
  public campoObrigatorio: string = "Campo Obrigatório"

  public parcelaFormGroup: FormGroup;
  public parcelasForms: FormGroup[] = [];
  public parcelasFormsGroup: FormGroup[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private dividasService: DividasService,
    private router: Router,
    private toastService: ToastService){
  }

  ngOnInit(): void {

    this.formGroup = this.formBuilder.group({
      nomeDoDevedor: ['', Validators.required],
      cpfDoDevedor: ['', Validators.required],
      numeroDoTitulo: ['', Validators.required],
      multa: ['', Validators.required],
      juros: ['', Validators.required],
    });

    this.parcelaFormGroup = this.formBuilder.group({
      numeroDaParcela: ['', Validators.required],
      valorDaParcela: ['', Validators.required],
      dataDeVencimento: ['', Validators.required],
    })

  }

  addDivida() {
    const novoFormGroup = this.formBuilder.group({
      numeroDaParcela: ['', Validators.required],
      valorDaParcela: [100, Validators.required],
      dataDeVencimento: [Date.now(), Validators.required],
    });

    this.parcelasForms.push(novoFormGroup);
  }

  removeParcela(): void {
    this.parcelasForms.pop();
  }

  salvarDivida() {
    this.formGroup.markAllAsTouched();

    this.parcelasForms.map(formGroup => formGroup.markAllAsTouched());

    const isInvalid = this.parcelasForms.some(formGroup => formGroup.invalid);

    if (this.formGroup.invalid) return;

    if (isInvalid) {
      this.toastService.showWarningMessage("Formulário de Parcela Inválido", "Divida");
      return;
    };

    if (this.parcelasForms.length == 0) {
      this.toastService.showWarningMessage("É obrigatório que uma divida, tenha ao menos uma parcela", "Divida");
      return;
    }

    const formValues = this.formGroup.getRawValue();

    const parcelas = this.parcelasForms.map(formGroup => formGroup.getRawValue());


    const values = {
      ...formValues,
      parcelas: parcelas
    };

    this.registrarDivida(values);
  }

  registrarDivida(divida: Divida){
    this.dividasService.registrarDivida(divida)
    .pipe(take(1))
    .subscribe(resp =>{
      if(resp.success){
        this.toastService.showSuccessMessage("Divida Cadastrada com sucesso", "Divida");

        setTimeout(() => {
          this.router.navigate(['features/dividas']);
        }, 1500);

      }
    });
  }
}
