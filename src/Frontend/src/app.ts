import { Endpoint, Rest } from 'aurelia-api'
import { inject, observable } from 'aurelia-framework'

@inject(Endpoint.of('api'))
export class App {
  message = 'Hello World!';

  @observable cases;

  @observable error;

  inProgress = false;

  constructor(private api: Rest) {

  }

  activate() {
    this.inProgress = true;
    this.api.find('/api/cases?query=ars.sprint.163').then(data => {
      this.cases = data;
      this.inProgress = false;
      this.message = `Cases total: ${data.length}`;
    })
    .catch(reason => {
      this.error = JSON.stringify(reason)
      this.inProgress = false;
    });
  }
}
