import { Component, OnInit } from '@angular/core';
import { MenuItem } from './Menu/menu.model';
import { MENU } from './Menu/menu';

@Component({
  selector: 'app-header-menu',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderMenuComponent implements OnInit {
  public headerMenus: MenuItem[];

  constructor() {
    this.headerMenus = MENU.filter(menu => menu.isTopbarMenu);
  }

  ngOnInit(): void {}
}
