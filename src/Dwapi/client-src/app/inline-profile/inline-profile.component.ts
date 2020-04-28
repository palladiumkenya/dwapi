import {animate, Component, OnInit, state, style, transition, trigger} from '@angular/core';


@Component({
  selector: 'liveapp-inline-profile',
  templateUrl: './inline-profile.component.html',
  styleUrls: ['./inline-profile.component.scss'],
    animations: [
        trigger('menu', [
            state('hidden', style({
                height: '0px'
            })),
            state('visible', style({
                height: '*'
            })),
            transition('visible => hidden', animate('400ms cubic-bezier(0.86, 0, 0.07, 1)')),
            transition('hidden => visible', animate('400ms cubic-bezier(0.86, 0, 0.07, 1)'))
        ])
    ]
})
export class InlineProfileComponent implements OnInit {

    active: boolean;

  constructor() { }

  ngOnInit() {
  }

    onClick(event) {
        this.active = !this.active;
        event.preventDefault();
    }
}
