import { Component, ElementRef, Input, OnInit, ViewChild } from "@angular/core";
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { DividasService } from "../services/dividas.service";
import { Divida } from "../models/divida";
import { take, tap } from "rxjs";
import { ToastService } from "../services/toast.service";
import { MASK_TYPES } from "src/app/shared/constants/format";
import { TextMaskConfig } from "angular2-text-mask";
import { Parcela } from "../models/parcela";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
  selector: 'app-dividas-edit',
  templateUrl: './dividas-edit.component.html',
  styleUrls: ['./dividas-edit.component.scss'],
})
export class DividasEditComponent implements OnInit {
  @Input() dividaId: string;
  public formGroup: FormGroup;
  public formParcelaGroup: FormGroup;
  public MASK = MASK_TYPES;
  public errors: any[];
  public parcelasForms: FormGroup[] = [];
  private _id: string;
  public campoObrigatorio: string = "Campo Obrigatório"

  public divida: Divida;


  constructor(
    private formBuilder: FormBuilder,
    private dividasService: DividasService,
    private route: ActivatedRoute,
    private router: Router,
    private toastService: ToastService){
      this._id = this.route.snapshot.params['id'];
      this.divida = new Divida();
  }

  async ngOnInit(): Promise<void> {
    this.formGroup = this.formBuilder.group({
      id: [''],
      diasEmAtraso: [''],
      nomeDoDevedor: ['', Validators.required],
      cpfDoDevedor: ['', Validators.required],
      numeroDoTitulo: ['', Validators.required],
      multa: ['', Validators.required],
      juros: ['', Validators.required],
    });

    let id = this.dividaId ? this.dividaId : this._id;
    await this.getDivida(id);
  }

  getDivida(id: string): Promise<any> {
    return new Promise((resolve, reject) => {
      this.dividasService.obterPorId(id)
      .pipe(take(1))
      .subscribe(response => {
        this.formGroup.patchValue(response);
        this.formGroup.patchValue({
          id: id
        });
        this.createParcelasFormGroup(response);
        resolve(response);
      }, error => {
        reject(error);
      });
    });
  }

  createParcelasFormGroup(divida: Divida): void {
    for (const parcela of divida.parcelas) {
      const parcelaFormGroup: FormGroup = this.formBuilder.group({
        valorDaParcela: [parcela.valorDaParcela, Validators.required],
        dataDeVencimento: [parcela.dataDeVencimento, Validators.required],
        numeroDaParcela: [parcela.numeroDaParcela, Validators.required],
        diasEmAtraso: [parcela.diasEmAtraso]
      });
      this.parcelasForms.push(parcelaFormGroup);
    }
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
    this.dividasService.atualizarDivida(divida)
    .pipe(take(1))
    .subscribe(resp =>{
      if(resp.success){
        this.toastService.showSuccessMessage("Divida Atualizada com sucesso", "Divida");

        setTimeout(() => {
          this.router.navigate(['features/dividas']);
        }, 1500);
      }
    });
  }


 get viewButton(): boolean{
  if(this.dividaId){
    return false;
  }
  else{
    return true;
  }
 }
}
