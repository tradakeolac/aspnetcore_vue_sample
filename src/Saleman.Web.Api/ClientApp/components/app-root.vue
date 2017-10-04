<template>
    <div id="app" class="container-fluid">
        <div class="row">
            <header-component params="route: route"></header-component>
        </div>
        <div class="row">
            <router-view></router-view>
        </div>
        <footer-component></footer-component>
    </div>
</template>

<script>
    import Vue from 'vue'
    import CounterExample from './counter-example'
    import FetchData from './fetch-data'
    import HomePage from './home-page'
    import Header from './layout/header'
    import Footer from './layout/footer'
    import LoginPage from './accounts/login-page'

    Vue.component('header-component', Header);
    Vue.component('footer-component', Footer);

    Vue.component('counter-example', CounterExample);
    Vue.component('fetch-data', FetchData);
    Vue.component('home-page', HomePage);
    Vue.component('login-page', LoginPage);

    export default {
        data() {
            return {
            }
        }
    }
</script>
