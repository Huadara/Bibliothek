<template>
    <div>
        <h1>Bibliothek</h1>
        <div>
            <b-button v-b-toggle.collapse-1 size="lg" block variant="primary">Add Customer</b-button>
            <b-collapse id="collapse-1" class="mb-2">
                <addcustomer-component></addcustomer-component>
            </b-collapse>
        </div>
        <div>
            <b-button v-b-toggle.collapse-2 size="lg" block variant="primary">Add Book</b-button>
            <b-collapse id="collapse-2" class="mb-2">
                <addbook-component></addbook-component>
            </b-collapse>
        </div>
        <div>
            <b-button v-b-toggle.collapse-3 size="lg" block variant="primary">Add Supplier</b-button>
            <b-collapse id="collapse-3" class="mb-2">
                <addsupplier-component></addsupplier-component>
            </b-collapse>
        </div>
        <div>
            <b-button v-b-toggle.collapse-4 size="lg" block variant="primary">Purchase</b-button>
            <b-collapse id="collapse-4" class="mb-2">
                <purchase-component></purchase-component>
            </b-collapse>
        </div>
        <div>
            <b-button v-b-toggle.collapse-5 size="lg" block variant="primary">Lending</b-button>
            <b-collapse id="collapse-5" class="mb-2">
                <lending-component></lending-component>
            </b-collapse>
        </div>
        <div>
            <b-button v-b-toggle.collapse-6 size="lg" block variant="primary">Restore</b-button>
            <b-collapse id="collapse-6" class="mb-2">
                <restore-component></restore-component>
            </b-collapse>
        </div>
    </div>
</template>

<script>
import AddCustomerComponent from '../components/AddCustomerComponent'
import AddBookComponent from '../components/AddBookComponent'
import AddSupplierComponent from '../components/AddSupplierComponent'
import PurchaseComponent from '../components/PurchaseComponent'
import LendingComponent from '../components/LendingComponent'
import RestoreComponent from '../components/RestoreComponent'

export default {
  name: 'MainPage',
  components: {
    'addcustomer-component': AddCustomerComponent,
    'addbook-component': AddBookComponent,
    'addsupplier-component': AddSupplierComponent,
    'purchase-component': PurchaseComponent,
    'lending-component': LendingComponent,
    'restore-component': RestoreComponent
  }
}
</script>

<style scoped>

</style>
