<template>
  <div>
      <div>
        <b-button v-b-toggle.collapse-1 variant="primary">Add Customer</b-button>
        <b-collapse id="collapse-1" class="mt-2">
          <addcustomer-component></addcustomer-component>
        </b-collapse>
      </div>
      <div>
        <b-button v-b-toggle.collapse-2 variant="primary">Add Book</b-button>
        <b-collapse id="collapse-2" class="mt-2">
          <addbook-component></addbook-component>
        </b-collapse>
      </div>
      <div>
        <b-button v-b-toggle.collapse-3 variant="primary">Add Supplier</b-button>
        <b-collapse id="collapse-3" class="mt-2">
          <addsupplier-component></addsupplier-component>
        </b-collapse>
      </div>

    <h1 class="display-4">LENDING</h1>
    <lending-component></lending-component>
  </div>

</template>

<script>
    import LendingComponent from "../components/LendingComponent";
    import AddCustomerComponent from "../components/AddCustomerComponent"
    import AddBookComponent from "../components/AddBookComponent";
    import AddSupplierComponent from "../components/AddSupplierComponent";
    import BuyBookComponent from "../components/BuyBookComponent";
    export default {
        name: "MainPage",
      components: {
          "lending-component": LendingComponent,
          "addcustomer-component": AddCustomerComponent,
          "addbook-component": AddBookComponent,
          "addsupplier-component": AddSupplierComponent,
          "buybook-component": BuyBookComponent
        }
    }
</script>

<style scoped>

</style>
