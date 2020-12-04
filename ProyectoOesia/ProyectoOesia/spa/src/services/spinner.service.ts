
export class SpinnerPluginService {
  constructor(private store: any) {}

  public showSpinner() {
    this.store.dispatch('showSpinner');
  }

  public removeSpinner() {
    this.store.dispatch('removeSpinner');
  }
}
