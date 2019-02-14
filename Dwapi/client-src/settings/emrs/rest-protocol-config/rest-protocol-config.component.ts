import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {EmrSystem} from "../../model/emr-system";

@Component({
  selector: 'liveapp-rest-protocol-config',
  templateUrl: './rest-protocol-config.component.html',
  styleUrls: ['./rest-protocol-config.component.scss']
})
export class RestProtocolConfigComponent implements OnInit {

    @Input() selectedEmr: EmrSystem;
    @Output() settingSavedChange = new EventEmitter();

  constructor() { }

  ngOnInit() {
  }

}
