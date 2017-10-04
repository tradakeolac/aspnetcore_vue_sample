<template>
    <!--header_top-->
        <div class="container">
            <div class="row">
                <div class="col-sm-6">
                    <div class="contactinfo">
                        <ul class="nav nav-pills" >
                            <!--<li><a href="#"><i class="fa fa-phone"></i> +2 95 01 88 821</a></li>
                            <li><a href="#"><i class="fa fa-envelope"></i> info@domain.com</a></li>-->
                            <li v-for="contact in contacts">
                                <a v-bind:href="(contact.type == 'email' ? 'mailto:' : 'tel:') + contact.information">
                                    <i v-bind:class="['fa ', contact.type == 'email' ? 'fa-envelope': 'fa-phone']"></i>
                                    {{contact.information}}
                                 </a>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="social-icons pull-right">
                        <ul class="nav navbar-nav">
                            <li v-for="social in socials">
                                <a v-bind:href="social.information"><i v-bind:class="['fa', 'fa-' + social.name]"></i></a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
</template>
<script>

    import { mapGetters, mapActions } from 'vuex'

    export default {
        computed: mapGetters({
            contacts: 'contactInformation',
            socials: 'socialInformation'
        }),
        methods: mapActions(['fetchStoreInformation']),
        created() {
            this.$store.dispatch('fetchStoreInformation', '6f3bcddf-66f4-481e-81ed-b098c13864d2')
        }
    }
</script>
