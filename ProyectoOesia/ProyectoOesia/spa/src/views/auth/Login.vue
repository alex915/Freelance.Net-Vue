<template>
  <section class="login">
    <div id="login" class="padding_top">
      <div>
        <v-row justify="center">
          <v-col cols="12" md="11" lg="10">
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
                <v-col cols="12">
                  <v-text-field
                    class="input"
                    v-model="email"
                    :outlined="true"
                    label="Email"
                    hint="Introduzca su email"
                    :loading="showLoading"
                    autocomplete="username"
                  ></v-text-field>
                </v-col>
              </v-row>
              <v-row justify="center">
                <v-col cols="12">
                  <v-text-field
                    v-model="password"
                    outlined
                    autocomplete="current-password"
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
                  <v-btn color="#2f9fe9" type="submit">Iniciar Sesion</v-btn>
                </v-col>
              </v-row>
            </form>
          </v-col>
        </v-row>
      </div>
    </div>
  </section>
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
        const token = response.data.token;
        const rol = response.data.rol;
        this.$store.dispatch("setToken", token);
        this.$store.dispatch("setRol", rol);
        authService.getUser().then((x) => {
          this.$spinner.removeSpinner();
          this.$store.dispatch("setUser", x.data);
          console.log(x.data);
          if (x.data.company.id != -1) {
            this.$router.push({ name: "New Company" });
          } else {
            this.$router.push({ name: "Home" });
          }
        });
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
.login {
  height: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
}
.padding_top {
  /* height: 80vh; */
  display: flex;
  justify-content: center;
  align-items: center;
}
.padding_top > div {
  color: white;
  padding: 80px 40px;
  background: rgb(17, 37, 87);
  background: linear-gradient(
    308deg,
    rgba(17, 37, 87, 1) 4%,
    rgba(17, 45, 154, 1) 24%,
    rgba(61, 160, 228, 1) 59%,
    rgba(18, 155, 241, 1) 100%
  );
  border-radius: 6px;
  box-shadow: 1px 2px 12px #ddd;
}

::v-deep input,
::v-deep label,
::v-deep fieldset,
::v-deep .theme--light {
  color: white !important;
}
</style>
