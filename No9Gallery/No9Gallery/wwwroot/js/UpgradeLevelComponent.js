var UpgradeLevelComponent = new Vue({
    el: '#upgrade',

    data() {        
            flag: false
    },

    methods:
    {
        ale: function () {
            if (confirm("Are you sure you want to use 5000 coins to upgrade?")) {
                alert("Upgrade success!");
                this.flag = true;
                axios.post('/PerssonInfo/Upgrade/')
            }
        }
    }
});