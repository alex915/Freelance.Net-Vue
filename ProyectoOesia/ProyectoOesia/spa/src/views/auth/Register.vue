<template>
  <form action class="form" @submit.prevent="register">
 <v-text-field
        v-model="email"
        :outlined="true"
        label="Email"
        hint="Introduzca su email"
        :loading="showLoading"
      ></v-text-field>
      <v-text-field v-model="password" :outlined="true"
        label="Password"
        hint="Introduzca su password"
        :loading="showLoading"></v-text-field>
      <v-text-field v-model="confirmPassword" :outlined="true"
        label="Confirm Password"
        hint="Confirme su password"
        :loading="showLoading"></v-text-field>
      <v-btn color="primary" type="submit">Register</v-btn>
      
  </form>
</template>

<script lang="ts">
 import { authService } from "@/services/auth.service";
import { Component, Vue } from "vue-property-decorator";


@Component
export default class Login extends Vue {
  public email!: string;
  public password!: string;
  public confirmPassword!: string;
  public showLoading!: boolean;

  public created() {
    this.email = '';
    this.password = '';
    this.confirmPassword = '';
    this.showLoading = false;
  }

  public register(): void {
    
    this.showLoading = true;
    authService
    .register(this.email, this.password)
    .then(() => {
      this.$router.push({ name: 'Login'});
    })
    .catch((error: Error) => {
      console.log(error);
    })
    .finally(() => {
      this.showLoading = false;
    });
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
