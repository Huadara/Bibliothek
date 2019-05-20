<template>
  <b-container>
    <b-row>
        <b-col>
          <div>
            <h3>Customer</h3>
            <input class="form-control" placeholder="Search Customer ID">
          </div>
        </b-col>
        <b-col>

            <b-dropdown v-bind:text="get_name(currentCustomer)" id="whitespace">
              <b-dropdown-item v-for="customer in customers" :key="customer.firstname" @click="selectedCustomerChanged(customer)">
                {{get_name(customer)}}
              </b-dropdown-item>
            </b-dropdown>
        </b-col>
    </b-row>
    <b-row>
        <b-col>
          <b-button variant="primary" @click="sendCustomer">Send Customer ID</b-button>
        </b-col>
    </b-row>
  </b-container>
</template>

<script>
    export default {
        name: "LendingComponent",
        methods:{

          selectedCustomerChanged(customer){
            this.currentCustomer = customer;
          },
          get_name(customer){
            if(customer === undefined) return "SEARCHING...";
            return customer.firstname + " " + customer.lastname;
           },
      },
      data(){
        return{
          currentCustomer: Object,
          customers: [],
          filter: ""
        }
      }
    }
</script>

<style scoped>
  #whitespace{
      padding-top: 2.6em;
  }
</style>
