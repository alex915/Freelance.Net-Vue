<template>
  <div id="login" class="padding_top">
    <v-row justify="center">
      <v-col cols="11" md="10" lg="8">
        <form action class="form" @submit.prevent="login">
          <v-row justify="center" class="pb-6">
            <v-col cols="12">
              <h2>¡Bienvenido de nuevo!</h2>
            </v-col>
            <v-col cols="12">
              <h4>Escribe tus detalles abajo</h4>
            </v-col>
          </v-row>
          <v-row justify="center">
            <v-col cols="11" sm="8" md="6" lg="6">
              <v-text-field
                v-model="email"
                :outlined="true"
                label="Email"
                hint="Introduzca su email"
                :loading="showLoading"
              ></v-text-field>
            </v-col>
          </v-row>
          <v-row justify="center">
            <v-col cols="11" sm="8" md="6" lg="6">
              <v-text-field
                v-model="password"
                outlined
                label="Password"
                hint="Introduzca su password"
                :loading="showLoading"
                :append-icon="show ? 'mdi-eye' : 'mdi-eye-off'"
                :type="show ? 'text' : 'password'"
                @keyup.enter="submit"
                @click:append="show = !show"
              ></v-text-field>
            </v-col>
          </v-row>
          <v-row justify="center">
            <v-col>
              <v-btn color="primary" type="submit">Iniciar Sesion</v-btn>
            </v-col>
          </v-row>
        </form>
      </v-col>
    </v-row>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { authService } from "@/services/auth.service";
import { AxiosResponse } from "axios";

@Component
export default class Login extends Vue {
  //propiedades del v-model
  public email = "";
  public password = "";
  public showLoading = false;
  public show = false;
  public items!: object[];

  //metodos de la clase Login
  public login(): void {
    //inicia el spinner
    this.showLoading = true;
    this.$spinner.showSpinner();
    //llama al metodo login del servicio y resuelve la promesa
    authService
      .login(this.email, this.password)
      .then((response: AxiosResponse) => {
        //si hay exito guarda el token en la store y redirige a home
        const token = response.data;
        this.$store.dispatch("setToken", token);
        this.$router.push({ name: "Home" });
      })
      .catch((error: Error) => {
        //si da error, lo muestra por consola
        console.log(error);
      })
      .finally(() => {
        //siempre se va a ejecutar. Esconde un spinner
        this.showLoading = false;
        this.$spinner.removeSpinner();
      });
    console.log(this.email, this.password);
  }
}
</script>

<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
.padding_top {
  padding: 40px 0;
  margin: 40px 0;
  background: rgb(9, 121, 100);
  background: linear-gradient(
    279deg,
    rgba(9, 121, 100, 0.2) 27%,
    rgba(73, 238, 180, 0.2) 84%,
    rgba(0, 212, 255, 0.2) 100%
  );
  visibility: 10px;
}
</style>
