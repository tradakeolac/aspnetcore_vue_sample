<template>
    <div class="main-nav">
        <header id="header">
            <!--header-->
            <div class="header_top">
                <top-header></top-header>
            </div>
            <div class="header-middle">
                <mid-header></mid-header>
            </div>
            <div class="header-bottom">
                <bottom-header></bottom-header>
            </div>
            <!--/header-bottom-->
        </header>
        <!--/header-->
    </div>
</template>

<script>
    import { routes } from './../../routes'
    import Vue from 'vue'
    import TopHeader from './header-top'
    import MidHeader from './header-mid.vue'
    import BottomHeader from './header-bottom.vue'

    export default {
        components: { TopHeader, MidHeader, BottomHeader }
    }
</script>

