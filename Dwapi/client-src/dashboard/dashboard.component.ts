import { Component, OnInit } from '@angular/core';
import {BreadcrumbService} from '../app/breadcrumb.service';

@Component({
  selector: 'liveapp-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

    constructor(private breadcrumbService: BreadcrumbService) {
        this.breadcrumbService.setItems([
            {label: ''},
        ]);
    }

  ngOnInit() {
  }

}
