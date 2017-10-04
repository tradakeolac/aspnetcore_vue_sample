<template>
    <div class="signup-form"><!--sign up form-->
        <h2>New User Signup!</h2>
        <form action="#">
            <!--<input type="text" placeholder="Name" />-->
            <input type="email" placeholder="Email Address" v-model="registerViewModel.email" name="email"/>
            <input type="password" placeholder="Password" name="password"  v-model="registerViewModel.password"/>
            <input type="password" placeholder="Confirm Password" name="confirmpassword"  v-model="registerViewModel.confirmPassword"/>
            <button type="button" class="btn btn-default" @click="register(registerViewModel)">Signup</button>
        </form>
    </div>
</template>
<script>

    import { mapGetters, mapActions } from 'vuex'

    export default {
        computed: mapGetters({
            registerViewModel: 'registerModel'
        }),

        methods: mapActions(['register'])
    }

</script>
