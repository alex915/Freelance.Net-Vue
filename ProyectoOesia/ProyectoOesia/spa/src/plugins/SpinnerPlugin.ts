import Store from '@/store';
import { SpinnerPluginService } from '@/services/spinner.service';

export const spinnerPlugin = {
  install(vue: any) {
    vue.prototype.$spinner = new SpinnerPluginService(Store);
  },
};
