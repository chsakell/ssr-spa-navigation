
const initState = {
    message: '',
    offers: [],
    article: {},
    betofday: '',
    sidebar: [],
    displaySidebar: true,
    header: '',
    footer: '',
    live: '',
    ignoreFirstOffersCall: false,
    ignoreFirstArticleCall: false,
    ignoreFirstHomeCall: false,
    ignoreFirstLeagueCall: false
};

const store = new Vuex.Store({
    state: {
        message: '',
        offers: [],
        article: {},
        league: {},
        betofday: '',
        sidebar: [],
        displaySidebar: true,
        header: '',
        footer: '',
        live: '',
        ignoreFirstOffersCall: false,
        ignoreFirstArticleCall: false,
        ignoreFirstHomeCall: false,
        ignoreFirstLeagueCall: false
    },
    mutations: {
        INIT_STATE: (state, initState) => {
            store.replaceState(Object.assign({}, store.state, initState));
        },
        SET_MESSAGE: (state, msg) => {
            state.message = msg;
        },
        SET_OFFERS: (state, offers) => {
            state.offers = offers;
        },
        SET_ARTICLE: (state, article) => {
            state.article = article;
        },
        SET_LEAGUE: (state, league) => {
            state.league = league;
        },
        SET_BETOFDAY: (state, betofday) => {
            state.betofday = betofday;
        },
        SET_HEADER: (state, header) => {
            state.header = header;
        },
        SET_FOOTER: (state, footer) => {
            state.footer = footer;
        },
        SET_SIDEBAR: (state, sports) => {
            state.sidebar = sports;
            state.displaySidebar = true;
        },
        IGNORE_OFFERS_API: (state, ignore) => {
            state.ignoreFirstOffersCall = ignore;
        },
        IGNORE_ARTICLE_API: (state, ignore) => {
            state.ignoreFirstArticleCall = ignore;
        },
        IGNORE_HOME_API: (state, ignore) => {
            state.ignoreFirstHomeCall = ignore;
        },
        IGNORE_LEAGUE_API: (state, ignore) => {
            state.ignoreFirstLeagueCall = ignore;
        },
        SET_SIDEBAR_VISIBLE: (state, visible) => {
            state.displaySidebar = visible;
        },
        SET_LIVE_BET: (state, liveBet) => {
            state.live = liveBet;
        }
    },
    getters: {
        getOffers: state => state.offers,
        getArticle: state => state.article,
        getSports: state => state.sidebar,
        displaySidebar: state => state.displaySidebar,
        getBetofday: state => state.betofday,
        getHeader: state => state.header,
        getFooter: state => state.footer,
        getLeague: state => state.league,
        getLiveBet: state => state.live
    }
});

const headerComponent = Vue.component('gml-header',
    {
        template: "<div style='background-color: #1A60A8;height: 104px;text-align:center; color:white;padding-top: 10px;'><h1>{{this.$store.state.header}}</h1></div>"
    });

const footerComponent = Vue.component('gml-footer',
    {
        template: "<h2 style='clear:both;background-color: #2f333a;color:white;padding:20px;'>{{this.$store.state.footer}}</h2>"
    });

const sidebarComponent = Vue.component('gml-sidebar',
    {
        template: "<div style='background-color:white;width: 218px;float: left;padding: 20px;margin-right: 10px;border-bottom: 1px solid #ebedf0;margin-top: 10px;'>" +
            "<h3 style='margin: 0 0 5px;font-size: 16px;line-height: 1em;font-weight: 500;color: #A8A8A8;'>Sports</h3>" +
            "<div v-for='sport in sports'>" + 
            "<h4>{{sport.displayName}} </h4>" +
            "<div v-for='league in sport.leagues' style='margin-left: 4px;margin-bottom:4px; border-bottom: 1px solid #ebedf0; cursor:pointer;padding: 10px 0px;' v-on:click='goTo(sport,league)'>" +
            "<span style='color:#0086ce'>{{league.title}}</span> <br/>" +
            "<span style='color: #a8a8a8;'>{{league.type}}</span>" +
            " </div>" +
            "</div>" +
            "</div>",
        props: ['sports'],
        methods: {
            goTo: function (sport, league) {
                this.$store.commit('IGNORE_LEAGUE_API', false)
                var uri = '/api/league/' + sport.id + '/' + league.id;

                // Checking structural required components..
                uri = buildUrl(this, uri);

                axios
                    .get(uri)
                    .then(response => {
                        this.$router.push({
                            path: '/league/' + sport.id + '/' + league.id,
                            params: { sport: sport.id, id: league.id }
                        });
                        document.title = response.data.d.league.title;
                        this.$store.commit('SET_LEAGUE', response.data.d.league);
                        // Structural commits..
                        updateStructures(this, response);
                    });
                }
            }
        });

const homeComponent = Vue.component('home', {
    data: function () {
        return {
            count: 0
        }
    },
    template: "<div style='float:left;width:80%;margin: 10px 0;'><img src='/images/home.png' style='display:inline-block;width:auto' /></div>",
    created() {
        this.$store.commit('SET_SIDEBAR_VISIBLE', true);
        document.title = "Legal and safe online betting";

        var uri = '/api/';
        
        // Checking structural required components..
        uri = buildUrl(this, uri);

        if (!this.$store.state.ignoreFirstHomeCall) {
            axios
                .get(uri)
                .then(response => {
                   // Structural commits..
                   updateStructures(this, response);  
                })
        } else {
            this.$store.commit('IGNORE_HOME_API', false);
        }
    }
})

const offerComponent = Vue.component('offers', {
    data: function () {
        return {
            count: 0
        }
    },
    computed: {
        offers() {
            return this.$store.getters.getOffers
        }
    },
    template: "<div style='float:left;width:80%;margin-bottom: 10px;background:white;margin-top:10px'>" +
                "<div v-for='offer in offers'>" +
                    "<div style='width: 50%;float: left;position:relative;text-align:center;margin:10px 0px'>" +
                        "<img v-bind:src='offerImage(offer.image)' v-on:click='goTo(offer.name)' style='cursor:pointer;height:208px;width:333px'/>" +
                    "<div style='position: absolute;bottom: 0; background: #185AA2;width: 333px; left:61px; font-size: 18px; color: white; text-align: center;'> {{ offer.title }}</div>" +
                    "</div> " +
                "</div> " +
            "</div>",
    created() {
        this.$store.commit('SET_SIDEBAR_VISIBLE', true);
        document.title = "Offers | Sports betting";
        if (!this.$store.state.ignoreFirstOffersCall) {
            var uri = '/api/offers';

            // Checking structural required components..
            uri = buildUrl(this, uri);         

            axios
                .get(uri)
                .then(response => {
                    this.$store.commit('SET_OFFERS', response.data.d.offers);

                    // Structural commits..
                    updateStructures(this, response);  
                })
        } else {
            this.$store.commit('IGNORE_OFFERS_API', false)
        }
    },
    methods: {
        goTo: function (article) {
            console.log(article);
            this.$router.push({ path: '/offers/' + article, params: { id: article }})
        },
        offerImage: function(image) {
            return '/images/' + image;
        }
    }
})

const buildUrl = (component, url) => {
    
    var structures = component.$router.currentRoute.meta.requiredStructures;

    // Checking structural required components..
    structures.forEach(conf => {
        var type =  typeof(component.$store.getters[conf.getter]);
        var value = component.$store.getters[conf.getter];
        console.log(conf);
        console.log(conf.title + ' : ' +  component.$store.getters[conf.getter] + ' : ' + type);
        if( 
            (type === 'string' && value !== '') || 
            (type === 'object' && Array.isArray(value) && value.length > 0)
        ){
            url = updateQueryStringParameter(url, conf.alias, true);
        }
    });

    console.log(url);
    return url;
}

function updateStructures(component, response) {
    
    var structures = component.$router.currentRoute.meta.requiredStructures;

    structures.forEach(conf => {

        conf.commits.forEach(com => {
            if(response.data.s[com.property]) {
                console.log('found ' + com.property + ' component..');
                component.$store.commit(com.mutation, response.data.s[com.property]);
                console.log('comitted action: ' + com.mutation);
            }
        });
    });
}

const articleComponent = Vue.component('offer-article', {
    template: '<div v-if="article" style="float:left;margin-bottom: 10px;background: white;margin-top: 25px; padding: 0 10px 10px;">' +
                  '<h1 style="margin-top: 35px;">{{article.title}}</h1>' +
                  '<h4>ID: {{article.id}}</h4>' +
                  '<div style="padding: 20px 0;">{{article.content}}</div>' +
                    "<div style='width:100%'>" +
                        "<img v-if='betofday' v-bind:src='betofday' style='width:auto' />" +
                    "</div>" +
              '</div>',
    computed: {
        article() {
            return this.$store.getters.getArticle
        },
        betofday() {
            return this.$store.getters.getBetofday == '' ? null : '/images/' + this.$store.getters.getBetofday
        }
    },
    created() {
        console.log(this.$router.currentRoute)
        this.$store.commit('SET_SIDEBAR_VISIBLE', false);
        if (!this.$store.state.ignoreFirstArticleCall) {
            var uri = '/api/offers/' + this.$route.params.id;
            
            // Checking structural required components..
            uri = buildUrl(this, uri);

            axios
                .get(uri)
                .then(response => {
                    document.title = response.data.d.article.title;
                    this.$store.commit('SET_ARTICLE', response.data.d.article);
                    this.$store.commit('SET_BETOFDAY', response.data.d.betofday);

                    // Structural commits..
                    updateStructures(this, response);  
                });
        } else {
            this.$store.commit('IGNORE_ARTICLE_API', false)
        }
    }
})

const leagueComponent = Vue.component('league', {
    template: "<div style='float:left; margin-top: 10px;'>" +
                "<div style='margin-bottom:10px'><img v-bind:src='liveBetImage' style='width: auto;'/></div>" +
                "<div style='float:left;width:100%;margin-bottom: 10px;'><img v-bind:src='leagueImage' style='width: auto;'/></div>" +
                "</div>",
    computed: {
        leagueImage() {
            return '/images/' + this.$store.getters.getLeague.image
        },
        liveBetImage() {
            return '/images/' + this.$store.getters.getLiveBet
        }
    },
    created() {

    }
})

const RouteStructureConfigs = {
    defaultStructure : [
        { title: 'header', alias: 'h', getter: 'getHeader', commits: [ { property: 'header', mutation: 'SET_HEADER' }] },
        { title: 'footer', alias: 'f', getter: 'getFooter', commits: [ { property: 'footer', mutation: 'SET_FOOTER' }] },
        { title: 'sidebar', alias: 's', getter: 'getSports', commits: [ { property: 'sidebar', mutation: 'SET_SIDEBAR' }]}
    ],
    noSidebarStructure : [
        { title: 'header', alias: 'h', getter: 'getHeader', commits: [ { property: 'header', mutation: 'SET_HEADER' }] },
        { title: 'footer', alias: 'f', getter: 'getFooter', commits: [ { property: 'footer', mutation: 'SET_FOOTER' }]}
    ],
    liveBetStructure: [
        { title: 'live', alias: 'l', getter: 'getLiveBet', commits: [{ property: 'live', mutation: 'SET_LIVE_BET' }] }
    ]
}

const router = new VueRouter({
    mode: 'history',
    routes: [
        { name: 'home', path: '/', component: homeComponent, 
            meta: 
            {
                requiredStructures: RouteStructureConfigs.defaultStructure
            } 
        },
        { name: 'offers', path: '/offers', component: offerComponent,
            meta:
            {
                requiredStructures: RouteStructureConfigs.defaultStructure
            } 
        },
        { name: 'article', path: '/offers/:id', component: articleComponent,
            meta:
            {
                requiredStructures: RouteStructureConfigs.noSidebarStructure
            }
        },
        {
            name: 'league', path: '/league/:sport/:id', component: leagueComponent,
            meta:
            {
                requiredStructures: [...RouteStructureConfigs.defaultStructure, ...RouteStructureConfigs.liveBetStructure]
            }
        }
    ]
})

var app = new Vue({
    store,
    router,
    el: '#app',
    data: {},
    computed: {
        sports() {
            return this.$store.getters.getSports
        },
        displaySidebar() {
            return this.$store.getters.displaySidebar
        }
    },
    components: {
        'gml-header': headerComponent,
        'gml-footer': footerComponent,
        'gml-sidebar': sidebarComponent
    },
    created() {
        var objStr = document.getElementById('initial_state').innerText;
        var initialState = JSON.parse(objStr);

        store.commit("INIT_STATE", initialState);
    }
})

store.commit('SET_MESSAGE', 'Hello V3..');

function updateQueryStringParameter(uri, key, value) {
    var re = new RegExp("([?&])" + key + "=.*?(&|$)", "i");
    var separator = uri.indexOf('?') !== -1 ? "&" : "?";
    if (uri.match(re)) {
        return uri.replace(re, '$1' + key + "=" + value + '$2');
    }
    else {
        return uri + separator + key + "=" + value;
    }
}
