export abstract class Loadable {

    private _loadableFragments: LoadableFragment = {
      default: false
    };
  
    constructor(fragments: string[] = []) {
      this.setFragments(fragments);
    }
  
    setFragments(fragments: string[]) {
      fragments.forEach(fragment => {
        this._loadableFragments[fragment] = false;
      });
    }
  
    get isLoading(): boolean {
      return this._loadableFragments.default;
    }
  
    get loadableFragments(): LoadableFragment {
      return this._loadableFragments;
    }
  
    enableLoading(fragment: string = 'default') {
      this._loadableFragments[fragment] = true;
    }
  
    disableLoading(fragment: string = 'default') {
      this._loadableFragments[fragment] = false;
    }
  }
  
  declare interface LoadableFragment {
    [name: string]: boolean;
  }
  