<template>
  <div>
      <div>
        <b-button v-b-toggle.collapse-1 variant="primary">Add Customer</b-button>
        <b-collapse id="collapse-1" class="mt-2">
          <addcustomer-component></addcustomer-component>
        </b-collapse>
      </div>

    <h1 class="display-4">LENDING</h1>
    <lending-component></lending-component>
  </div>

</template>

<script>
    import LendingComponent from "../components/LendingComponent";
    import AddCustomerComponent from "../components/AddCustomerComponent"
    export default {
        name: "MainPage",
      components: {
          "lending-component": LendingComponent,
          "addcustomer-component": AddCustomerComponent
        }
    }
</script>

<style scoped>

</style>
