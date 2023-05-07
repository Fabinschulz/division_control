import { Component, Input, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: 'app-dividas-view',
  templateUrl: './dividas-view.component.html',
  styleUrls: ['./dividas-view.component.scss'],
})
export class DividasViewComponent implements OnInit {
  public _id: string;

  ngOnInit(): void {
    console.log(this._id)
  }

  constructor(  private route: ActivatedRoute){
    this._id = this.route.snapshot.params['id'];
  }
}
