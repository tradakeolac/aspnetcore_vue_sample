<template>
   	<footer id="footer"><!--Footer-->
		<div class="footer-top">
            <top-footer></top-footer>
        </div>
        <div class="footer-widget">
            <mid-footer></mid-footer>
        </div>
        <div class="footer-bottom">
            <bottom-footer></bottom-footer>
        </div>
    </footer>
</template>

<script>

    import Vue from 'vue'
    import TopFooter from './footer-top.vue'
    import MidFooter from './footer-mid.vue'
    import BottomFooter from './footer-bottom.vue'

    export default {
        components: {
            TopFooter, MidFooter, BottomFooter
        }
    }
</script>
