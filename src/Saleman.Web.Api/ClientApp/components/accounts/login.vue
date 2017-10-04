<template>
    <div class="login-form"><!--login form-->
        <h2>Login to your account</h2>
        <form action="#">
            <input type="email" placeholder="Email Address" v-model="loginViewModel.email"/>
            <input type="password" placeholder="Password" name="password"  v-model="loginViewModel.password"/>
            <span>
                <input type="checkbox" class="checkbox" v-model="loginViewModel.remember">
                Keep me signed in
            </span>
            <button type="button" class="btn btn-default" @click="login(loginViewModel)">Login</button>
        </form>
    </div><!--/login form-->
</template>
<script>
    import { mapGetters, mapActions } from 'vuex'

    export default {
        computed: mapGetters({
            loginViewModel: 'loginModel'
        }),

        methods: mapActions(['login'])
    }
</script>
