<template>

	<section id="form"><!--form-->
		<div class="container">
			<div class="row">
				<div class="col-sm-4 col-sm-offset-1">
                    <login></login>
				</div>
				<div class="col-sm-1">
					<h2 class="or">OR</h2>
				</div>
				<div class="col-sm-4">
                    <register></register>
				</div>
			</div>
		</div>
	</section><!--/form-->

</template>

<script>

    import  Register from './register.vue'
    import Login from './login.vue'

    export default {
        components: { Register, Login }
    }

</script>
