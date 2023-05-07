import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup } from "@angular/forms";
import { Router } from "@angular/router";
import { DividasService } from "../services/dividas.service";
import { Divida } from "../models/divida";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { DividasEditComponent } from "../dividas-edit/dividas-edit.component";
import { ToastService } from "../services/toast.service";

@Component({
  selector: 'app-dividas-list',
  templateUrl: './dividas-list.component.html',
  styleUrls: ['./dividas-list.component.scss'],
})
export class DividasListComponent implements OnInit {

  public dividas: Divida[] = [];


  ngOnInit(): void {
    this.obterLista();
  }

  constructor(
    private router: Router,
    private toastService: ToastService,
    private dividasService: DividasService){
  }

  criar() {
    this.router.navigate([`features/dividas/dividas-create`]);
  }

  editar(id: string) {
    this.router.navigate([`features/dividas/dividas-edit/${id}`]);
  }

  remover(id: string) {
    this.dividasService.removerDivida(id)
      .subscribe(response => {
        if(response) {
          this.toastService.showSuccessMessage("Divida removida com sucesso", "Divida");
          setTimeout(() => {
           this.obterLista();
          }, 1500);
        }
    })
  }

  visualizar(id: string){
    this.router.navigate(['features/dividas/dividas-view', id]);
  }

  obterLista(){
    this.dividasService.obterDividas()
      .subscribe(response => {
       this.dividas = response;
       console.log(this.dividas)
    })
  }

}
