import {Component, Input, OnInit} from '@angular/core';
import {EmrSystem} from '../model/emr-system';

@Component({
  selector: 'liveapp-db-protocol',
  templateUrl: './db-protocol.component.html',
  styleUrls: ['./db-protocol.component.scss']
})
export class DbProtocolComponent implements OnInit {

    @Input() selectedEmr: EmrSystem;

    public constructor() {
    }

    public ngOnInit() {
    }

}
