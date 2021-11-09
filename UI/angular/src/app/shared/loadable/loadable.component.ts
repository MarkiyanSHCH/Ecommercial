import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-loadable',
  templateUrl: './loadable.component.html',
  styleUrls: ['./loadable.component.css']
})
export class LoadableComponent{

  @Input() active = false;
}
