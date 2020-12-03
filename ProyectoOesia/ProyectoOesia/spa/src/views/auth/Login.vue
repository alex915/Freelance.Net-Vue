<template>
  <div id="login" class="hello">
    <form action class="form" @submit.prevent="login">
      <v-text-field
        v-model="email"
        :outlined="true"
        label="Email"
        hint="Introduzca su email"
        :loading="showLoading"
      ></v-text-field>
      <v-text-field v-model="password" outlined
        label="Password"
        hint="Introduzca su password"
        :loading="showLoading"></v-text-field>
      <v-btn color="success" type="submit">Login</v-btn>
    </form>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { authService } from "@/services/auth.service";

@Component
export default class Login extends Vue {
    //propiedades del v-model
  public email!: string;
  public password!: string;
  public showLoading!: boolean;
  public items!: object[];
    //inicializacion de las propiedades del v-model
  public created() {
    this.email = "";
    this.password = "";
    this.showLoading = false;   
  }
    //metodos de la clase Login
    public login(): void {
      //inicia el spinner
    this.showLoading = true;

    //llama al metodo login del servicio y resuelve la promesa
    authService
    .login(this.email, this.password)
        .then((response: any) => {
        //si hay exito guarda el token en la store y redirige a home
      const token = response.data;
      this.$store.dispatch('setToken', token);
      this.$router.push({ name: 'Home'});
    })
        .catch((error: Error) => {
        //si da error, lo muestra por consola
       console.log(error);
       alert(error);
    })
        .finally(() => {
        //siempre se va a ejecutar. Esconde un spinner
      this.showLoading = false;
    })
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
</style>
