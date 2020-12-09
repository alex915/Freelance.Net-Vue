
<template>
  <v-container grid-list-md>
    <v-layout row wrap>
      <v-flex xs12 sm6>
        <v-card><!--info de la BBDD-->
          <v-img src="">
            <v-card-title>
                <h2>Nombre empresa</h2>
            </v-card-title>
            <v-card-text>
                <p>Descripción de la empresa</p>
            </v-card-text>
            <v-card-action>
                <v-btn color="info" flat>Contacto</v-btn>
          </v-img>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>
import Vue from 'vue';
import {companyService} from '@/services/company.service';
export default  Vue.extend({
 <script lang="ts">
import { companyService, Vue } from 'vue-property-decorator';

@Component({
  components: {
  },
})
export default class Home extends Vue {
  public get token(): string {
    return this.$store.getters['getToken'];
  }
}
</script>