using System.Text.Json.Serialization;

namespace SkolplattformenElevApi.Models.Internal.Sharepoint
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    internal class Action
    {
        public string type { get; set; }
        public Parameters parameters { get; set; }
    }

    internal class AlternativeUrlMap
    {
        public string UserPhotoAspx { get; set; }
        public string MediaTAThumbnailPathUrl { get; set; }
        public string MediaTAThumbnailHostUrl { get; set; }
        public string AFDCDNEnabled { get; set; }
        public string CurrentSiteCDNPolicy { get; set; }
        public string PublicCDNEnabled { get; set; }
        public string PrivateCDNEnabled { get; set; }

     }


    internal class AppBarParams
    {
        public bool isDisabled { get; set; }
        public bool isCreateAffordanceEnabled { get; set; }
        public string portalUrl { get; set; }
        public object mySiteHostUrl { get; set; }
        public object logoHash { get; set; }
        public object homeSiteNavConfigs { get; set; }
        public string correlationID { get; set; }
        public CompanyPortalReference companyPortalReference { get; set; }
        public bool isCurrentSiteAHomeSite { get; set; }
        public bool isHomeSiteEnabled { get; set; }
        public bool router { get; set; }
        public bool isGlobalNavEnabled { get; set; }
        public Strings strings { get; set; }
        public bool useAppBarDirect { get; set; }
        public bool useAppBarLists { get; set; }
    }

    internal class BannerImageUrl
    {
        public string Description { get; set; }
        public string Url { get; set; }
    }

    internal class BaseUrl
    {
        public SpfxDependency spfxDependency { get; set; }
        public bool spfxLink { get; set; }
    }

    internal class BotDrivenAdaptiveCardExtension
    {
        public string type { get; set; }
        public string path { get; set; }
    }

    internal class CallToActionLink
    {
        public SpfxDependency spfxDependency { get; set; }
    }

    internal class CallToActionText
    {
        public bool spfxSearchablePlainText { get; set; }
    }

    internal class CardButtonAction
    {
        public string title { get; set; }
        public string style { get; set; }
        public Action action { get; set; }
        public bool isVisible { get; set; }
    }

    internal class CardDesignerAce
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class CardSelectionAction
    {
        public string type { get; set; }
        public Parameters parameters { get; set; }
    }

    internal class CarouselSettings
    {
        public bool autoplay { get; set; }
        public int autoplaySpeed { get; set; }
        public bool dots { get; set; }
        public bool lazyLoad { get; set; }
        public bool metadata { get; set; }
        public bool swipe { get; set; }
        public bool useStockItems { get; set; }
    }

    internal class CategoriesPages
    {
        public int Version { get; set; }
        public bool Enabled { get; set; }
    }

    internal class Child
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public bool IsDocLib { get; set; }
        public bool IsExternal { get; set; }
        public int ParentId { get; set; }
        public int ListTemplateType { get; set; }
        public object AudienceIds { get; set; }
        public int CurrentLCID { get; set; }
        public List<object> Children { get; set; }
        public object OpenInNewWindow { get; set; }
    }

    internal class ClientPersistedCacheKey
    {
        public CurrentKey CurrentKey { get; set; }
        public PreviousKey PreviousKey { get; set; }
    }

    internal class CompanyPortalReference
    {
        public string SiteId { get; set; }
        public string WebId { get; set; }
    }

    internal class Content
    {
        public string CanvasContent1 { get; set; }
        public string LayoutWebpartsContent { get; set; }
        public AlternativeUrlMap AlternativeUrlMap { get; set; }
        public object CommentsDisabled { get; set; }
    }

    internal class Content2
    {
        public Title title { get; set; }
        public CallToActionText callToActionText { get; set; }
        public Image image { get; set; }
        public PreviewImage previewImage { get; set; }
        public Link link { get; set; }
        public CallToActionLink callToActionLink { get; set; }
    }

    internal class ContentCenterEverywhereFeature
    {
        public int Version { get; set; }
        public bool Enabled { get; set; }
    }

    internal class ContentCenterFeature
    {
        public int Version { get; set; }
        public bool Enabled { get; set; }
    }

    internal class ContextWebInfo
    {
        public string _ObjectType_ { get; set; }
        public int FormDigestTimeoutSeconds { get; set; }
        public object FormDigestValue { get; set; }
        public string LibraryVersion { get; set; }
        public string SiteFullUrl { get; set; }
        public List<string> SupportedSchemaVersions { get; set; }
        public string WebFullUrl { get; set; }
    }

    internal class Current
    {
        public BaseUrl baseUrl { get; set; }
        public SiteId siteId { get; set; }
        public WebId webId { get; set; }
        public ListId listId { get; set; }
        public LastListId lastListId { get; set; }
        public List<Site> sites { get; set; }
        public LinkUrl linkUrl { get; set; }
        public ImageSource2 imageSource { get; set; }
        public UniqueId uniqueId { get; set; }
        public List<Item> items { get; set; }
        public Title title { get; set; }
        public Description description { get; set; }
        public ImageURL imageURL { get; set; }
        public Url url { get; set; }
        public List<Content> content { get; set; }
        public List<Person> persons { get; set; }
        public List<Image> images { get; set; }
        public SelectedListId selectedListId { get; set; }
        public SelectedViewId selectedViewId { get; set; }
        public SelectedListUrl selectedListUrl { get; set; }
        public WebRelativeListUrl webRelativeListUrl { get; set; }
    }

    internal class CurrentKey
    {
        public string Key { get; set; }
        public string Date { get; set; }
    }

    internal class DaylightDate
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int DayOfWeek { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }
        public int Milliseconds { get; set; }
    }

    internal class Description
    {
        public string @default { get; set; }
        public bool spfxSearchablePlainText { get; set; }
    }

    internal class DriveInfo
    {
    }

    internal class EEDashboard
    {
        public int Version { get; set; }
        public bool Enabled { get; set; }
    }

    internal class Email
    {
        public SpfxDependency spfxDependency { get; set; }
    }

    internal class FarmSettings
    {
        public string ExternalService_powerappswebhostname { get; set; }
        public string ExternalService_powerappscreatehostname { get; set; }
        public string ExternalService_flowhostname { get; set; }
        public string ExternalService_flowservicehostname { get; set; }
        public string ExternalService_popularplatformsenable { get; set; }
        public string ExternalService_filerequestenable { get; set; }
        public string ExternalService_primaryfileingest { get; set; }
        public string ExternalService_workingset { get; set; }
        public string ExternalService_yggdrasil { get; set; }
        public string ExternalService_isplannerintegrationsupported { get; set; }
        public string ExternalService_islivepersonacardenabled { get; set; }
        public string ExternalService_powerappsappshostname { get; set; }
        public string ExternalService_powerappsmakehostname { get; set; }
        public string ExternalService_bizappsapprovalflowsenabled { get; set; }
        public string ExternalService_bizappsflowenabled { get; set; }
        public string ExternalService_bizappsflowforlibrariesenabled { get; set; }
        public string ExternalService_bizappsflowintegrationenabled { get; set; }
        public string ExternalService_bizappshubsitejoinapprovalenabled { get; set; }
        public string ExternalService_bizappspawebpartenabled { get; set; }
        public string ExternalService_bizappspowerappsenabled { get; set; }
        public string ExternalService_bizappsremindmeflowenabled { get; set; }
        public string ExternalService_bizappsrequestsignoffenabled { get; set; }
        public string ExternalService_isnotificationservicesupported { get; set; }
        public string ExternalService_modernsiteanalyticsenable { get; set; }
        public string ExternalService_pagecontentapprovalenabled { get; set; }
        public string ExternalService_videousecdnenabled { get; set; }
        public string ExternalService_powerbihostname { get; set; }
        public string ExternalService_isstreamwebpartsupported { get; set; }
        public string ExternalService_excelgraphenabled { get; set; }
        public string ExternalService_powerbiapibaseurl { get; set; }
        public string ExternalService_searchcloudenvironment { get; set; }
        public string ExternalService_isodbsubstratesearchenabled { get; set; }
        public string ExternalService_isitemscopefilecardenabled { get; set; }
        public string ExternalService_isstockimagesupported { get; set; }
        public string ExternalService_issphomemicroservicesupported { get; set; }
        public string ExternalService_searchcloudurl { get; set; }
        public string ExternalService_vivaconnectionsenabled { get; set; }
        public string ExternalService_yourphoneapiendpoint { get; set; }
        public string ExternalService_yourphonehvcendpoint { get; set; }
        public string ExternalService_augloopeditorserviceenabled { get; set; }
        public string ExternalService_phonelinkapiendpoint { get; set; }
        public string ExternalService_phonelinkhvcendpoint { get; set; }
        public string ExternalService_phonelinkcdnendpoint { get; set; }
    }

    internal class FeatureInfo
    {
        public SitePages SitePages { get; set; }
        public SitePagesResources SitePagesResources { get; set; }
        public RecommendedItems RecommendedItems { get; set; }
        public MultilingualPages MultilingualPages { get; set; }
        public MultilingualResources MultilingualResources { get; set; }
        public Viewers Viewers { get; set; }
        public SitePagePublishing SitePagePublishing { get; set; }
        public SitePagesScheduling SitePagesScheduling { get; set; }
        public SitePagesSchedulingResources SitePagesSchedulingResources { get; set; }
        public ModernAudienceTargeting ModernAudienceTargeting { get; set; }
        public FeedVideo FeedVideo { get; set; }
        public FeedVideoResources FeedVideoResources { get; set; }
        public Publishing Publishing { get; set; }
        public FollowingContent FollowingContent { get; set; }
        public CategoriesPages CategoriesPages { get; set; }
        public MixedReality MixedReality { get; set; }
        public MixedRealityResources MixedRealityResources { get; set; }
        public EEDashboard EEDashboard { get; set; }
        public ContentCenterFeature ContentCenterFeature { get; set; }
        public ContentCenterEverywhereFeature ContentCenterEverywhereFeature { get; set; }
    }

    internal class FeedVideo
    {
        public int Version { get; set; }
        public bool Enabled { get; set; }
    }

    internal class FeedVideoResources
    {
        public int Version { get; set; }
        public bool Enabled { get; set; }
    }

    internal class FilterBy
    {
    }

    internal class FirstFlushPerf
    {
        public string MondoExecutionTime { get; set; }
    }

    internal class FollowingContent
    {
        public int Version { get; set; }
        public bool Enabled { get; set; }
    }

    internal class GridSettings
    {
        public int imageSize { get; set; }
        public int imageCropping { get; set; }
        public int imageAspectRatio { get; set; }
        public bool lightbox { get; set; }
    }

    internal class Group
    {
        public string @default { get; set; }
    }

    internal class Guids
    {
        public SiteId siteId { get; set; }
        public ListId listId { get; set; }
        public WebId webId { get; set; }
        public UniqueId uniqueId { get; set; }
    }

    internal class Id
    {
        public SpfxDependency spfxDependency { get; set; }
    }

    internal class Image
    {
        public Guids guids { get; set; }
        public Url url { get; set; }
        public ResolvedUrl resolvedUrl { get; set; }
     //   public ImageUrl imageUrl { get; set; }
        public Id id { get; set; }
        public WebId webId { get; set; }
        public SiteId siteId { get; set; }
        public ListId listId { get; set; }
    }

    internal class Image3
    {
        public SiteId siteId { get; set; }
        public WebId webId { get; set; }
        public ListId listId { get; set; }
        public Id id { get; set; }
        public Url url { get; set; }
    }

    internal class ImageSource2
    {
        public bool spfxImageSource { get; set; }
        public SpfxDependency spfxDependency { get; set; }
    }

    internal class ImageURL
    {
        public SpfxDependency spfxDependency { get; set; }
    }

    internal class ImageUrl2
    {
        public SpfxDependency spfxDependency { get; set; }
    }

    internal class Item
    {
        [JsonPropertyName("@odata.context")]
        public string OdataContext { get; set; }

        [JsonPropertyName("@odata.type")]
        public string OdataType { get; set; }

        [JsonPropertyName("@odata.id")]
        public string OdataId { get; set; }

        [JsonPropertyName("@odata.etag")]
        public string OdataEtag { get; set; }

        [JsonPropertyName("@odata.editLink")]
        public string OdataEditLink { get; set; }
        public int FileSystemObjectType { get; set; }
        public int Id { get; set; }
        public string ContentTypeId { get; set; }
        public object OData__ModerationComments { get; set; }
        public object ComplianceAssetId { get; set; }
        public string Title { get; set; }
        public string PageLayoutType { get; set; }
        public BannerImageUrl BannerImageUrl { get; set; }
        public string Description { get; set; }
        public double PromotedState { get; set; }
        public DateTime FirstPublishedDate { get; set; }
        public string LayoutWebpartsContent { get; set; }
        public List<int> OData__AuthorBylineId { get; set; }
        public string OData__TopicHeader { get; set; }
        public object OData__SPSitePageFlags { get; set; }
        public object OData__OriginalSourceUrl { get; set; }
        public object OData__OriginalSourceSiteId { get; set; }
        public object OData__OriginalSourceWebId { get; set; }
        public object OData__OriginalSourceListId { get; set; }
        public object OData__OriginalSourceItemId { get; set; }
        public object OData__SPCallToAction { get; set; }
        public int ID { get; set; }
        public DateTime Created { get; set; }
        public int AuthorId { get; set; }
        public DateTime Modified { get; set; }
        public int EditorId { get; set; }
        public int OData__ModerationStatus { get; set; }
        public object CheckoutUserId { get; set; }
        public string UniqueId { get; set; }
        public int owshiddenversion { get; set; }
        public string OData__UIVersionString { get; set; }
        public string GUID { get; set; }
    }

    internal class Item2
    {
        public Image image { get; set; }
        public SourceItem sourceItem { get; set; }
        public Title title { get; set; }
        public RawPreviewImageUrl rawPreviewImageUrl { get; set; }
    }

    internal class ItemProperties
    {
    }

    internal class ItemReference
    {
        public SiteId SiteId { get; set; }
        public WebId WebId { get; set; }
    }
    
    internal class LastListId
    {
        public SpfxDependency spfxDependency { get; set; }
    }

    internal class LearningAssignmentsAce
    {
        public string type { get; set; }
        public string path { get; set; }
    }

    internal class Link
    {
        public SpfxDependency spfxDependency { get; set; }
    }

    internal class LinkUrl
    {
        public bool spfxLink { get; set; }
        public SpfxDependency spfxDependency { get; set; }
    }

    internal class ListId
    {
        public SpfxDependency spfxDependency { get; set; }
    }

    internal class ListPermsMask
    {
        public int High { get; set; }
        public int Low { get; set; }
    }

    internal class LoaderConfig
    {
        public List<string> internalModuleBaseUrls { get; set; }
        public string entryModuleId { get; set; }
        public ScriptResources scriptResources { get; set; }
        public string exportName { get; set; }
    }

    internal class Manifest
    {
        public PreloadOptions preloadOptions { get; set; }
        public Title title { get; set; }
        public Description description { get; set; }
        public string assemblyId { get; set; }
        public bool hasSuiteNav { get; set; }
        public string version { get; set; }
        public string alias { get; set; }
        public List<string> preloadComponents { get; set; }
        public bool isInternal { get; set; }
        public LoaderConfig loaderConfig { get; set; }
        public int manifestVersion { get; set; }
        public string id { get; set; }
        public string componentType { get; set; }
        public List<PreconfiguredEntry> preconfiguredEntries { get; set; }
        public bool? hiddenFromToolbox { get; set; }
        public bool? canUpdateConfiguration { get; set; }
        public bool? supportsThemeVariants { get; set; }
        public PropertiesMetadata propertiesMetadata { get; set; }
        public List<string> searchablePropertyNames { get; set; }
        public List<string> linkPropertyNames { get; set; }
        public List<string> imageLinkPropertyNames { get; set; }
        public bool? supportsFullBleed { get; set; }
        public bool? disabledOnClassicSharepoint { get; set; }
        public List<string> supportedHosts { get; set; }
    }

    internal class MenuData
    {
        public List<SettingsDatum> SettingsData { get; set; }
        public string SignOutUrl { get; set; }
    }

    internal class MicrosoftLoadThemedStyles
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MicrosoftOfficeUiFabricReactBundle
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MicrosoftSpAdaptiveCardExtensionBase
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MicrosoftSpApplicationBase
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MicrosoftSpComponentBase
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MicrosoftSpCoreLibrary
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MicrosoftSpDiagnostics
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MicrosoftSpDialog
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MicrosoftSpExtensionBase
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MicrosoftSpHttp
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MicrosoftSpImageHelper
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MicrosoftSpLoader
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MicrosoftSpLodashSubset
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MicrosoftSpPageContext
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MicrosoftSpPropertyPane
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MicrosoftSpWebpartBase
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MixedReality
    {
        public int Version { get; set; }
        public bool Enabled { get; set; }
    }

    internal class MixedRealityResources
    {
        public int Version { get; set; }
        public bool Enabled { get; set; }
    }

    internal class ModernAudienceTargeting
    {
        public int Version { get; set; }
        public bool Enabled { get; set; }
    }

    internal class MsI18nUtilities
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsOdspCoreBundle
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsOdspUtilitiesBundle
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsSpA11y
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsSpAdaptiveCardExtensionIsolationComponentBase
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsSpCanvasEditShared
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsSpCanvasRead
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsSpCarouselLayout
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsSpCoachmarkUtility
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsSpComponentUtilities
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsSpCustomerPromise
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsSpDataproviders
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsSpDeferredComponent
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsSpEditCustomerPromise
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsSpEventDataProvider
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsSpFlexLayout
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsSpGridLayout
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsSpHeroLayout
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsSpHostCommandHandler
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsSpHtmlEmbed
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsSpListDocumentLayout
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsSpListviewCommon
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsSpLivePersonaCard
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsSpMwtCardsTelemetry
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsSpMysitecache
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsSpNewsDataProvider
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsSpPageChrome
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsSpPagesAuthoringAreaDetector
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsSpPagesCore
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsSpPagesPreloads
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsSpRecommendedItems
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsSpRichImage
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsSpRte
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsSpSearchCommon
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsSpSuiteNav
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsSpTelemetry
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsSpTopicSdk
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsSpViewportLoader
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsSpWebpartShared
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsSpWebpartSharedEditmode
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MsUifabricStylingBundle
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class MultiGeoInfo
    {
        public string InstanceId { get; set; }
        public object DataLocation { get; set; }
        public bool IsDefaultDataLocation { get; set; }
        public string RootSiteUrl { get; set; }
        public string MySiteHostUrl { get; set; }
        public string TenantAdminUrl { get; set; }
        public string PortalUrl { get; set; }
        public object AdditionalUrls { get; set; }
    }

    internal class MultilingualPages
    {
        public int Version { get; set; }
        public bool Enabled { get; set; }
    }

    internal class MultilingualResources
    {
        public int Version { get; set; }
        public bool Enabled { get; set; }
    }

    internal class MwtApprovalsAce
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class MwtCommunicationsAce
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class MwtShiftsAce
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class MwtTasksAce
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class Name
    {
        public SpfxDependency spfxDependency { get; set; }
    }

    internal class NavigationInfo
    {
        public bool isAudienceTargeted { get; set; }
        public List<QuickLaunch> quickLaunch { get; set; }
        public List<object> topNav { get; set; }
    }

    internal class OfficeUiFabricReactBundle
    {
        public string type { get; set; }
        public string path { get; set; }
    }

    internal class OrganizationNewsSiteReference
    {
        public string SiteId { get; set; }
        public string WebId { get; set; }
    }

    internal class Page
    {
        public bool DoesUserHaveEditPermission { get; set; }
        public Content Content { get; set; }
    }

    internal class PagePermsMask
    {
        public int High { get; set; }
        public int Low { get; set; }
    }

    internal class Parameters
    {
        public string target { get; set; }
        public string view { get; set; }
    }

    internal class Perf
    {
        public string IisLatency { get; set; }
        public string spRequestDuration { get; set; }
        public string QueryCount { get; set; }
        public string QueryDuration { get; set; }
        public string CPUDuration { get; set; }
        public string ClaimsAuthenticationTime { get; set; }
        public string ClaimsAuthenticationTimeType { get; set; }

    }

    internal class Person
    {
        public Id id { get; set; }
        public Email email { get; set; }
        public Name name { get; set; }
    }

    internal class PreconfiguredEntry
    {
        public Title title { get; set; }
        public Description description { get; set; }
        public string groupId { get; set; }
        public Group group { get; set; }
        public string officeFabricIconFontName { get; set; }
        public Properties2 properties { get; set; }
        public string iconImageUrl { get; set; }
        public string cardSize { get; set; }
    }

    internal class PreloadOptions
    {
        public bool shouldPreloadUser { get; set; }
        public bool shouldPreloadItem { get; set; }
        public bool shouldPreloadQuickLaunch { get; set; }
        public string preloadListItemQueryOption { get; set; }
        public string preloadListItemQueryOptionOverride { get; set; }
        public List<string> preloadListItemProperties { get; set; }
    }

    internal class PreviewImage
    {
        public Url url { get; set; }
        public ResolvedUrl resolvedUrl { get; set; }
      //  public ImageUrl imageUrl { get; set; }
        public Id id { get; set; }
        public WebId webId { get; set; }
        public SiteId siteId { get; set; }
        public ListId listId { get; set; }
    }

    internal class PreviousKey
    {
        public string Key { get; set; }
        public string Date { get; set; }
    }

    internal class Properties2
    {
        public string label { get; set; }
        public string annotation { get; set; }
        public string authorName { get; set; }
        public string chartitem { get; set; }
        public string endrange { get; set; }
        public string excelSettingsType { get; set; }
        public string file { get; set; }
        public string listId { get; set; }
        public string modifiedAt { get; set; }
        public string photoUrl { get; set; }
        public string rangeitem { get; set; }
        public string serverRelativeUrl { get; set; }
        public string siteId { get; set; }
        public int? startPage { get; set; }
        public string startrange { get; set; }
        public string tableitem { get; set; }
        public string title { get; set; }
        public string uniqueId { get; set; }
        public bool? wdallowinteractivity { get; set; }
        public bool? wdhidegridlines { get; set; }
        public bool? wdhideheaders { get; set; }
        public string webId { get; set; }
        public string wopiurl { get; set; }
        public int? imageSourceType { get; set; }
        public string imageSource { get; set; }
        public string captionText { get; set; }
        public string altText { get; set; }
        public string linkUrl { get; set; }
        public string overlayText { get; set; }
        public string fileName { get; set; }
        public string imgWidth { get; set; }
        public string imgHeight { get; set; }
        public List<object> items { get; set; }
        public bool? isMigrated { get; set; }
        public string layoutId { get; set; }
        public string layoutComponentId { get; set; }
        public bool? shouldShowThumbnail { get; set; }
        public string url { get; set; }
        public string description { get; set; }
        public string imageURL { get; set; }
        public int? linkPreviewComponentMode { get; set; }
        public bool? displayLink { get; set; }
        public string heroLayoutThreshold { get; set; }
        public string carouselLayoutMaxWidth { get; set; }
        public string carouselLayoutComponentId { get; set; }
        public string heroLayoutComponentId { get; set; }
        public object layout { get; set; }
        public List<object> persons { get; set; }
        public int? translateX { get; set; }
        public int? translateY { get; set; }
        public string layoutType { get; set; }
        public string textAlignment { get; set; }
        public bool? showTopicHeader { get; set; }
        public bool? showPublishDate { get; set; }
        public string topicHeader { get; set; }
        public bool? enableGradientEffect { get; set; }
        public string customMessage { get; set; }
        public CarouselSettings carouselSettings { get; set; }
        public bool? showChrome { get; set; }
        public ShowNewsMetadata showNewsMetadata { get; set; }
        public string selectedListId { get; set; }
        public string selectedCategory { get; set; }
        public int? dateRangeOption { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public bool? isOnSeeAllPage { get; set; }
        public bool? audienceTarget { get; set; }
        public bool? showImage { get; set; }
        public GridSettings gridSettings { get; set; }
        public bool? isRecursive { get; set; }
        public bool? isCdnEnabledForList { get; set; }
        public bool? hasDynamicModeEnabled { get; set; }
        public int? maxImagesCount { get; set; }
        public List<object> images { get; set; }
        public bool? canAlwaysSelectDocLibAsSource { get; set; }
        public bool? isDocumentLibrary { get; set; }
        public FilterBy filterBy { get; set; }
        public int? maxItems { get; set; }
        public string dataFilter { get; set; }
        public string dataTypeFilter { get; set; }
        public bool? hideWebPartWhenEmpty { get; set; }
        public string iconProperty { get; set; }
        public string templateType { get; set; }
        public int? cardIconSourceType { get; set; }
        public int? cardImageSourceType { get; set; }
        public CardSelectionAction cardSelectionAction { get; set; }
        public int? numberCardButtonActions { get; set; }
        public List<CardButtonAction> cardButtonActions { get; set; }
        public List<QuickView> quickViews { get; set; }
        public bool? isQuickViewConfigured { get; set; }
        public int? currentQuickViewIndex { get; set; }
        public string dataType { get; set; }
        public string spRequestUrl { get; set; }
        public string graphRequestUrl { get; set; }
    }

    internal class PropertiesMetadata
    {
        public Current current { get; set; }
    }

    internal class Publishing
    {
        public int Version { get; set; }
        public bool Enabled { get; set; }
    }

    internal class QuickLaunch
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public bool IsDocLib { get; set; }
        public bool IsExternal { get; set; }
        public int ParentId { get; set; }
        public int ListTemplateType { get; set; }
        public object AudienceIds { get; set; }
        public int CurrentLCID { get; set; }
        public List<Child> Children { get; set; }
        public object OpenInNewWindow { get; set; }
    }

    internal class QuickView
    {
        public string data { get; set; }
        public string template { get; set; }
        public string id { get; set; }
        public string displayName { get; set; }
    }

    internal class RawPreviewImageUrl
    {
        public SpfxDependency spfxDependency { get; set; }
        public bool spfxImageSource { get; set; }
    }

    internal class React
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class ReactDom
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class RecommendedItems
    {
        public int Version { get; set; }
        public bool Enabled { get; set; }
    }

    internal class ResolvedUrl
    {
        public SpfxDependency spfxDependency { get; set; }
    }

    internal class NewsItemResponse
    {
        public InternalUser user { get; set; }
        public Item item { get; set; }
        public ItemProperties itemProperties { get; set; }
        public Page page { get; set; }
        public bool isUserVoiceEnabled { get; set; }
        public string templateFolderName { get; set; }
        public bool moderationEnabled { get; set; }
        public DateTime modifiedDate { get; set; }
        public int draftVersionVisibility { get; set; }
        public UserInfo userInfo { get; set; }
        public string clientSideApplicationId { get; set; }
        public ContextWebInfo contextWebInfo { get; set; }
        public List<object> customActions { get; set; }
        public SpPageContextInfo spPageContextInfo { get; set; }
        public string ApplicationAPIEndpoints { get; set; }
        public List<Manifest> manifests { get; set; }
        public string buildNumber { get; set; }
        public string appScriptLang { get; set; }
        public Perf perf { get; set; }
        public FirstFlushPerf firstFlushPerf { get; set; }
        public bool MediaTAForThumbnail { get; set; }
    }

    internal class SavedforlaterWebpart
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class ScriptResources
    {
        [JsonPropertyName("sp-pages")]
        public SpPages SpPages { get; set; }
        public Tslib tslib { get; set; }

        [JsonPropertyName("@ms/sp-telemetry")]
        public MsSpTelemetry MsSpTelemetry { get; set; }

        [JsonPropertyName("@ms/sp-suite-nav")]
        public MsSpSuiteNav MsSpSuiteNav { get; set; }

        [JsonPropertyName("@ms/sp-search-common")]
        public MsSpSearchCommon MsSpSearchCommon { get; set; }

        [JsonPropertyName("@microsoft/sp-application-base")]
        public MicrosoftSpApplicationBase MicrosoftSpApplicationBase { get; set; }

        [JsonPropertyName("@microsoft/sp-loader")]
        public MicrosoftSpLoader MicrosoftSpLoader { get; set; }

        [JsonPropertyName("@ms/odsp-core-bundle")]
        public MsOdspCoreBundle MsOdspCoreBundle { get; set; }

        [JsonPropertyName("@microsoft/office-ui-fabric-react-bundle")]
        public MicrosoftOfficeUiFabricReactBundle MicrosoftOfficeUiFabricReactBundle { get; set; }

        [JsonPropertyName("@ms/sp-pages-preloads")]
        public MsSpPagesPreloads MsSpPagesPreloads { get; set; }

        [JsonPropertyName("@microsoft/sp-lodash-subset")]
        public MicrosoftSpLodashSubset MicrosoftSpLodashSubset { get; set; }

        [JsonPropertyName("@microsoft/sp-core-library")]
        public MicrosoftSpCoreLibrary MicrosoftSpCoreLibrary { get; set; }

        [JsonPropertyName("@ms/sp-topic-sdk")]
        public MsSpTopicSdk MsSpTopicSdk { get; set; }

        [JsonPropertyName("@microsoft/sp-page-context")]
        public MicrosoftSpPageContext MicrosoftSpPageContext { get; set; }

        [JsonPropertyName("@ms/sp-edit-customer-promise")]
        public MsSpEditCustomerPromise MsSpEditCustomerPromise { get; set; }

        [JsonPropertyName("@ms/sp-mysitecache")]
        public MsSpMysitecache MsSpMysitecache { get; set; }

        [JsonPropertyName("@ms/sp-coachmark-utility")]
        public MsSpCoachmarkUtility MsSpCoachmarkUtility { get; set; }

        [JsonPropertyName("@microsoft/sp-webpart-base")]
        public MicrosoftSpWebpartBase MicrosoftSpWebpartBase { get; set; }
        public React react { get; set; }

        [JsonPropertyName("react-dom")]
        public ReactDom ReactDom { get; set; }

        [JsonPropertyName("@ms/uifabric-styling-bundle")]
        public MsUifabricStylingBundle MsUifabricStylingBundle { get; set; }

        [JsonPropertyName("@ms/sp-component-utilities")]
        public MsSpComponentUtilities MsSpComponentUtilities { get; set; }

        [JsonPropertyName("@microsoft/load-themed-styles")]
        public MicrosoftLoadThemedStyles MicrosoftLoadThemedStyles { get; set; }

        [JsonPropertyName("@ms/sp-pages-core")]
        public MsSpPagesCore MsSpPagesCore { get; set; }

        [JsonPropertyName("@ms/sp-deferred-component")]
        public MsSpDeferredComponent MsSpDeferredComponent { get; set; }

        [JsonPropertyName("@ms/sp-canvas-read")]
        public MsSpCanvasRead MsSpCanvasRead { get; set; }

        [JsonPropertyName("@microsoft/sp-diagnostics")]
        public MicrosoftSpDiagnostics MicrosoftSpDiagnostics { get; set; }

        [JsonPropertyName("@microsoft/sp-http")]
        public MicrosoftSpHttp MicrosoftSpHttp { get; set; }

        [JsonPropertyName("@ms/odsp-utilities-bundle")]
        public MsOdspUtilitiesBundle MsOdspUtilitiesBundle { get; set; }

        [JsonPropertyName("@ms/sp-customer-promise")]
        public MsSpCustomerPromise MsSpCustomerPromise { get; set; }

        [JsonPropertyName("@ms/sp-pages-authoring-area-detector")]
        public MsSpPagesAuthoringAreaDetector MsSpPagesAuthoringAreaDetector { get; set; }

        [JsonPropertyName("@ms/sp-a11y")]
        public MsSpA11y MsSpA11y { get; set; }

        [JsonPropertyName("@ms/sp-viewport-loader")]
        public MsSpViewportLoader MsSpViewportLoader { get; set; }

        [JsonPropertyName("bot-driven-adaptive-card-extension")]
        public BotDrivenAdaptiveCardExtension BotDrivenAdaptiveCardExtension { get; set; }

        [JsonPropertyName("@ms/sp-adaptive-card-extension-isolation-component-base")]
        public MsSpAdaptiveCardExtensionIsolationComponentBase MsSpAdaptiveCardExtensionIsolationComponentBase { get; set; }

        [JsonPropertyName("sp-anchor")]
        public SpAnchor SpAnchor { get; set; }

        [JsonPropertyName("sp-dataproviders")]
        public SpDataproviders SpDataproviders { get; set; }

        [JsonPropertyName("sp-contentrollup-webpart")]
        public SpContentrollupWebpart SpContentrollupWebpart { get; set; }

        [JsonPropertyName("@microsoft/sp-image-helper")]
        public MicrosoftSpImageHelper MicrosoftSpImageHelper { get; set; }

        [JsonPropertyName("@ms/sp-dataproviders")]
        public MsSpDataproviders MsSpDataproviders { get; set; }

        [JsonPropertyName("@ms/sp-webpart-shared")]
        public MsSpWebpartShared MsSpWebpartShared { get; set; }

        [JsonPropertyName("sp-siteactivity-webpart")]
        public SpSiteactivityWebpart SpSiteactivityWebpart { get; set; }

        [JsonPropertyName("@ms/i18n-utilities")]
        public MsI18nUtilities MsI18nUtilities { get; set; }

        [JsonPropertyName("sp-fileviewer-webparts")]
        public SpFileviewerWebparts SpFileviewerWebparts { get; set; }

        [JsonPropertyName("@microsoft/sp-component-base")]
        public MicrosoftSpComponentBase MicrosoftSpComponentBase { get; set; }

        [JsonPropertyName("@ms/sp-host-command-handler")]
        public MsSpHostCommandHandler MsSpHostCommandHandler { get; set; }

        [JsonPropertyName("sp-image-webpart-bundle")]
        public SpImageWebpartBundle SpImageWebpartBundle { get; set; }

        [JsonPropertyName("@ms/sp-rich-image")]
        public MsSpRichImage MsSpRichImage { get; set; }

        [JsonPropertyName("sp-quick-links-webpart")]
        public SpQuickLinksWebpart SpQuickLinksWebpart { get; set; }

        [JsonPropertyName("sp-newsreel-webpart-bundle")]
        public SpNewsreelWebpartBundle SpNewsreelWebpartBundle { get; set; }

        [JsonPropertyName("@ms/sp-news-data-provider")]
        public MsSpNewsDataProvider MsSpNewsDataProvider { get; set; }

        [JsonPropertyName("sp-newsfeed-webpart-bundle")]
        public SpNewsfeedWebpartBundle SpNewsfeedWebpartBundle { get; set; }

        [JsonPropertyName("sp-linkpreview-webpart")]
        public SpLinkpreviewWebpart SpLinkpreviewWebpart { get; set; }

        [JsonPropertyName("@ms/sp-html-embed")]
        public MsSpHtmlEmbed MsSpHtmlEmbed { get; set; }

        [JsonPropertyName("sp-telemetry")]
        public SpTelemetry SpTelemetry { get; set; }

        [JsonPropertyName("sp-hero-webpart-bundle")]
        public SpHeroWebpartBundle SpHeroWebpartBundle { get; set; }

        [JsonPropertyName("@ms/sp-hero-layout")]
        public MsSpHeroLayout MsSpHeroLayout { get; set; }

        [JsonPropertyName("sp-people-webparts-bundle")]
        public SpPeopleWebpartsBundle SpPeopleWebpartsBundle { get; set; }

        [JsonPropertyName("@ms/sp-grid-layout")]
        public MsSpGridLayout MsSpGridLayout { get; set; }

        [JsonPropertyName("sp-application-base")]
        public SpApplicationBase SpApplicationBase { get; set; }

        [JsonPropertyName("@microsoft/sp-extension-base")]
        public MicrosoftSpExtensionBase MicrosoftSpExtensionBase { get; set; }

        [JsonPropertyName("sp-title-region-webpart")]
        public SpTitleRegionWebpart SpTitleRegionWebpart { get; set; }

        [JsonPropertyName("sp-custommessageregion-bundle")]
        public SpCustommessageregionBundle SpCustommessageregionBundle { get; set; }

        [JsonPropertyName("sp-canvas-edit")]
        public SpCanvasEdit SpCanvasEdit { get; set; }

        [JsonPropertyName("@microsoft/sp-property-pane")]
        public MicrosoftSpPropertyPane MicrosoftSpPropertyPane { get; set; }

        [JsonPropertyName("@ms/sp-canvas-edit-shared")]
        public MsSpCanvasEditShared MsSpCanvasEditShared { get; set; }

        [JsonPropertyName("@ms/sp-rte")]
        public MsSpRte MsSpRte { get; set; }

        [JsonPropertyName("sp-toolbox")]
        public SpToolbox SpToolbox { get; set; }

        [JsonPropertyName("sp-news-webpart-bundle")]
        public SpNewsWebpartBundle SpNewsWebpartBundle { get; set; }

        [JsonPropertyName("sp-events-webpart")]
        public SpEventsWebpart SpEventsWebpart { get; set; }

        [JsonPropertyName("@ms/sp-flex-layout")]
        public MsSpFlexLayout MsSpFlexLayout { get; set; }

        [JsonPropertyName("@ms/sp-carousel-layout")]
        public MsSpCarouselLayout MsSpCarouselLayout { get; set; }

        [JsonPropertyName("@ms/sp-event-data-provider")]
        public MsSpEventDataProvider MsSpEventDataProvider { get; set; }

        [JsonPropertyName("sp-image-gallery-webpart-bundle")]
        public SpImageGalleryWebpartBundle SpImageGalleryWebpartBundle { get; set; }

        [JsonPropertyName("sp-list-webpart")]
        public SpListWebpart SpListWebpart { get; set; }

        [JsonPropertyName("@ms/sp-listview-common")]
        public MsSpListviewCommon MsSpListviewCommon { get; set; }

        [JsonPropertyName("sp-safehtml")]
        public SpSafehtml SpSafehtml { get; set; }

        [JsonPropertyName("sp-bingmap")]
        public SpBingmap SpBingmap { get; set; }

        [JsonPropertyName("sp-recommendeditems-webpart")]
        public SpRecommendeditemsWebpart SpRecommendeditemsWebpart { get; set; }

        [JsonPropertyName("@ms/sp-recommended-items")]
        public MsSpRecommendedItems MsSpRecommendedItems { get; set; }

        [JsonPropertyName("sp-newslink-webpart")]
        public SpNewslinkWebpart SpNewslinkWebpart { get; set; }

        [JsonPropertyName("@ms/sp-webpart-shared-editmode")]
        public MsSpWebpartSharedEditmode MsSpWebpartSharedEditmode { get; set; }

        [JsonPropertyName("sp-my-documents-webpart")]
        public SpMyDocumentsWebpart SpMyDocumentsWebpart { get; set; }

        [JsonPropertyName("@ms/sp-list-document-layout")]
        public MsSpListDocumentLayout MsSpListDocumentLayout { get; set; }

        [JsonPropertyName("sp-image-tools")]
        public SpImageTools SpImageTools { get; set; }

        [JsonPropertyName("sp-search")]
        public SpSearch SpSearch { get; set; }

        [JsonPropertyName("@ms/sp-live-persona-card")]
        public MsSpLivePersonaCard MsSpLivePersonaCard { get; set; }

        [JsonPropertyName("@ms/sp-page-chrome")]
        public MsSpPageChrome MsSpPageChrome { get; set; }

        [JsonPropertyName("office-ui-fabric-react-bundle")]
        public OfficeUiFabricReactBundle OfficeUiFabricReactBundle { get; set; }

        [JsonPropertyName("savedforlater-webpart")]
        public SavedforlaterWebpart SavedforlaterWebpart { get; set; }

        [JsonPropertyName("sp-pages-preloads")]
        public SpPagesPreloads SpPagesPreloads { get; set; }

        [JsonPropertyName("sp-search-common")]
        public SpSearchCommon SpSearchCommon { get; set; }

        [JsonPropertyName("sp-list-host")]
        public SpListHost SpListHost { get; set; }

        [JsonPropertyName("sp-adaptive-card-extension-web-part")]
        public SpAdaptiveCardExtensionWebPart SpAdaptiveCardExtensionWebPart { get; set; }

        [JsonPropertyName("@microsoft/sp-dialog")]
        public MicrosoftSpDialog MicrosoftSpDialog { get; set; }

        [JsonPropertyName("@microsoft/sp-adaptive-card-extension-base")]
        public MicrosoftSpAdaptiveCardExtensionBase MicrosoftSpAdaptiveCardExtensionBase { get; set; }

        [JsonPropertyName("sp-isolated-adaptive-card-extension-web-part")]
        public SpIsolatedAdaptiveCardExtensionWebPart SpIsolatedAdaptiveCardExtensionWebPart { get; set; }

        [JsonPropertyName("sp-topic-shared")]
        public SpTopicShared SpTopicShared { get; set; }

        [JsonPropertyName("sp-feedvideo-webpart")]
        public SpFeedvideoWebpart SpFeedvideoWebpart { get; set; }

        [JsonPropertyName("sp-multilingual")]
        public SpMultilingual SpMultilingual { get; set; }

        [JsonPropertyName("card-designer-ace")]
        public CardDesignerAce CardDesignerAce { get; set; }

        [JsonPropertyName("mwt-approvals-ace")]
        public MwtApprovalsAce MwtApprovalsAce { get; set; }

        [JsonPropertyName("mwt-communications-ace")]
        public MwtCommunicationsAce MwtCommunicationsAce { get; set; }

        [JsonPropertyName("@ms/sp-mwt-cards-telemetry")]
        public MsSpMwtCardsTelemetry MsSpMwtCardsTelemetry { get; set; }

        [JsonPropertyName("mwt-shifts-ace")]
        public MwtShiftsAce MwtShiftsAce { get; set; }

        [JsonPropertyName("mwt-tasks-ace")]
        public MwtTasksAce MwtTasksAce { get; set; }

        [JsonPropertyName("sp-page-cards")]
        public SpPageCards SpPageCards { get; set; }

        [JsonPropertyName("teams-ace")]
        public TeamsAce TeamsAce { get; set; }

        [JsonPropertyName("learning-assignments-ace")]
        public LearningAssignmentsAce LearningAssignmentsAce { get; set; }

        [JsonPropertyName("tslib-2-bundle")]
        public Tslib2Bundle Tslib2Bundle { get; set; }
    }

    internal class SelectedListId
    {
        public SpfxDependency spfxDependency { get; set; }
    }

    internal class SelectedListUrl
    {
        public SpfxDependency spfxDependency { get; set; }
    }

    internal class SelectedViewId
    {
        public SpfxDependency spfxDependency { get; set; }
    }

    internal class SettingsDatum
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
    }

    internal class ShowNewsMetadata
    {
        public bool showSocialActions { get; set; }
        public bool showAuthor { get; set; }
        public bool showDate { get; set; }
    }

    internal class Site
    {
        public ItemReference ItemReference { get; set; }
        public Url Url { get; set; }
    }

    internal class SiteId
    {
        public SpfxDependency spfxDependency { get; set; }
    }

    internal class SiteId2
    {
        public SpfxDependency spfxDependency { get; set; }
    }

    internal class SitePagePublishing
    {
        public int Version { get; set; }
        public bool Enabled { get; set; }
    }

    internal class SitePages
    {
        public int Version { get; set; }
        public bool Enabled { get; set; }
    }

    internal class SitePagesResources
    {
        public int Version { get; set; }
        public bool Enabled { get; set; }
    }

    internal class SitePagesScheduling
    {
        public int Version { get; set; }
        public bool Enabled { get; set; }
    }

    internal class SitePagesSchedulingResources
    {
        public int Version { get; set; }
        public bool Enabled { get; set; }
    }

    internal class SourceItem
    {
        public Guids guids { get; set; }
        public Url url { get; set; }
    }

    internal class SpAdaptiveCardExtensionWebPart
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class SpAnchor
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class SpApplicationBase
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class SpBingmap
    {
        public string type { get; set; }
        public string path { get; set; }
    }

    internal class SpCanvasEdit
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class SpContentrollupWebpart
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class SpCustommessageregionBundle
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class SpDataproviders
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class SpEventsWebpart
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class SpFeedvideoWebpart
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class SpFileviewerWebparts
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class SpfxDependency
    {
        public string dependencyType { get; set; }
        public string identifierType { get; set; }
        public string identifierUrlSubType { get; set; }
        public object defaultValue { get; set; }
        public object name { get; set; }
    }

    internal class SpHeroWebpartBundle
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class SpImageGalleryWebpartBundle
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class SpImageTools
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class SpImageWebpartBundle
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class SpIsolatedAdaptiveCardExtensionWebPart
    {
        public string type { get; set; }
        public string path { get; set; }
    }

    internal class SpLinkpreviewWebpart
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class SpListHost
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class SpListWebpart
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class SpMultilingual
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class SpMyDocumentsWebpart
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class SpNewsfeedWebpartBundle
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class SpNewslinkWebpart
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class SpNewsreelWebpartBundle
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class SpNewsWebpartBundle
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class SpPageCards
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class SpPageContextInfo
    {
        public int ListCountLimit { get; set; }
        public int FieldCountLimit { get; set; }
        public int ClientPrefetchBehavior { get; set; }
        public bool IsConsumerListsPaidUser { get; set; }
        public bool siteDisabled { get; set; }
        public string webServerRelativeUrl { get; set; }
        public string webAbsoluteUrl { get; set; }
        public string viewId { get; set; }
        public int webPropertyFlags2 { get; set; }
        public string listId { get; set; }
        public string listTemplateId { get; set; }
        public ListPermsMask listPermsMask { get; set; }
        public string listUrl { get; set; }
        public string listTitle { get; set; }
        public int listBaseTemplate { get; set; }
        public int listBaseType { get; set; }
        public string ariaCollectorUrl { get; set; }
        public string oneDsCollectorUrl { get; set; }
        public DriveInfo driveInfo { get; set; }
        public VanityUrls vanityUrls { get; set; }
        public List<MultiGeoInfo> multiGeoInfo { get; set; }
        public bool viewOnlyExperienceEnabled { get; set; }
        public bool m365GroupsBlockDownloadsExperienceEnabled { get; set; }
        public bool readOnlyExperienceEnabled { get; set; }
        public bool disableBackToClassic { get; set; }
        public bool isOAuth { get; set; }
        public bool isLocationserviceAvailable { get; set; }
        public bool blockDownloadsExperienceEnabled { get; set; }
        public bool idleSessionSignOutEnabled { get; set; }
        public bool activityBasedTimeoutEnabled { get; set; }
        public bool isFraudTenant { get; set; }
        public DateTime fraudTenantAccessRevokeTime { get; set; }
        public string cdnPrefix { get; set; }
        public object cdnBaseUrl { get; set; }
        public string siteAbsoluteUrl { get; set; }
        public string siteId { get; set; }
        public bool showNGSCDialogForSyncOnTS { get; set; }
        public bool supportPoundStorePath { get; set; }
        public bool supportPercentStorePath { get; set; }
        public string siteSubscriptionId { get; set; }
        public string tenantDisplayName { get; set; }
        public bool isMultiGeoTenant { get; set; }
        public bool isMultiGeoODBMode { get; set; }
        public string webDomain { get; set; }
        public bool IsIEDisabledForItemsScope { get; set; }
        public bool isSPO { get; set; }
        public bool isSelfServiceSiteCreationEnabled { get; set; }
        public string farmLabel { get; set; }
        public string farmName { get; set; }
        public string contentDBName { get; set; }
        public string serverRequestPath { get; set; }
        public string layoutsUrl { get; set; }
        public string webId { get; set; }
        public string webTitle { get; set; }
        public int WebTitleCurrentLCID { get; set; }
        public string webTemplate { get; set; }
        public string webTemplateConfiguration { get; set; }
        public string webDescription { get; set; }
        public int WebDescriptionCurrentLCID { get; set; }
        public string tenantAppVersion { get; set; }
        public bool isAppWeb { get; set; }
        public string webLogoUrl { get; set; }
        public int webLanguage { get; set; }
        public string webLanguageName { get; set; }
        public int currentLanguage { get; set; }
        public string currentUICultureName { get; set; }
        public string currentCultureName { get; set; }
        public int currentCultureLCID { get; set; }
        public string env { get; set; }
        public string env2 { get; set; }
        public string cloudType { get; set; }
        public int nid { get; set; }
        public int fid { get; set; }
        public DateTime serverTime { get; set; }
        public string siteClientTag { get; set; }
        public bool crossDomainPhotosEnabled { get; set; }
        public bool openInClient { get; set; }
        public int webUIVersion { get; set; }
        public WebPermMasks webPermMasks { get; set; }
        public string pageListId { get; set; }
        public int pageItemId { get; set; }
        public PagePermsMask pagePermsMask { get; set; }
        public int pagePersonalizationScope { get; set; }
        public string userEmail { get; set; }
        public int userId { get; set; }
        public string userLoginName { get; set; }
        public string userDisplayName { get; set; }
        public bool isAnonymousGuestUser { get; set; }
        public bool isEmailAuthenticationGuestUser { get; set; }
        public bool isExternalGuestUser { get; set; }
        public bool isNativeFederatedUser { get; set; }
        public object homeTenantId { get; set; }
        public bool requestFilesLinkEnabled { get; set; }
        public bool isShareByLinkEnabled { get; set; }
        public int FolderAnonymousLinkPermission { get; set; }
        public string systemUserKey { get; set; }
        public bool alertsEnabled { get; set; }
        public string siteServerRelativeUrl { get; set; }
        public string allowSilverlightPrompt { get; set; }
        public string themeCacheToken { get; set; }
        public string themedCssFolderUrl { get; set; }
        public ThemedImageFileNames themedImageFileNames { get; set; }
        public bool modernThemingEnabled { get; set; }
        public bool isSiteAdmin { get; set; }
        public bool isMySiteOwner { get; set; }
        public bool isSiteOwner { get; set; }
        public bool isSiteListsHost { get; set; }
        public string viewType { get; set; }
        public List<int> ExpFeatures { get; set; }
        public string experimentData { get; set; }
        public List<object> experimentDataLookup { get; set; }
     //   public KillSwitches killSwitches { get; set; }
        public string CorrelationId { get; set; }
        public bool hasManageWebPermissions { get; set; }
        public bool isNoScriptEnabled { get; set; }
        public object groupId { get; set; }
        public object relatedGroupId { get; set; }
        public bool isTeamsConnectedSite { get; set; }
        public bool isTeamsChannelSite { get; set; }
        public int teamsChannelType { get; set; }
        public string channelGroupId { get; set; }
        public bool groupHasHomepage { get; set; }
        public bool groupHasQuickLaunchConversationsLink { get; set; }
        public object departmentId { get; set; }
        public object hubSiteId { get; set; }
        public string sensitivityLabel { get; set; }
        public string sensitivityLabelName { get; set; }
        public object restrictedToRegion { get; set; }
        public bool hasPendingWebTemplateExtension { get; set; }
        public bool HasCortexTopicExperiencesCapability { get; set; }
        public bool disableRecommendedItems { get; set; }
        public bool isGroupRelatedSite { get; set; }
        public bool isArchived { get; set; }
        public object IBSegments { get; set; }
        public int IBMode { get; set; }
        public string spAppBarManifestScript { get; set; }
        public string spAppBarBackupManifestScript { get; set; }
        public string spAppBarManifestScriptIntegrity { get; set; }
        public string portalUrl { get; set; }
        public bool hasAutogeneratedWebLogo { get; set; }
        public bool canSyncHubSitePermissions { get; set; }
        public bool isHubSite { get; set; }
        public bool isWebWelcomePage { get; set; }
        public string siteClassification { get; set; }
        public bool hideSyncButtonOnODB { get; set; }
        public bool isEduClass { get; set; }
        public bool isEduClassProvisionPending { get; set; }
        public bool isEduClassProvisionChecked { get; set; }
        public bool isEduTenant { get; set; }
        public bool showNGSCDialogForSyncOnODB { get; set; }
        public bool disableListSync { get; set; }
        public bool sitePagesEnabled { get; set; }
        public int sitePagesFeatureVersion { get; set; }
        public bool showOpenInDesktopOptionForSyncedFiles { get; set; }
        public FeatureInfo featureInfo { get; set; }
        public string DesignPackageId { get; set; }
        public object groupType { get; set; }
        public object groupColor { get; set; }
        public object siteColor { get; set; }
        public int headerEmphasis { get; set; }
        public int headerLayout { get; set; }
        public int logoAlignment { get; set; }
        public object focalPoint { get; set; }
        public object headerLogoHash { get; set; }
        public object globalNavLogoHash { get; set; }
        public bool isGlobalNavEnabled { get; set; }
        public int searchScope { get; set; }
        public object homeSiteNavConfigs { get; set; }
        public int searchBoxInNavBar { get; set; }
        public object searchBoxPlaceholderText { get; set; }
        public bool IsClassicPageSearchUpgraded { get; set; }
        public bool WebTemplatesGalleryFirstRunEnabled { get; set; }
        public bool hasWebTemplateExtension { get; set; }
        public NavigationInfo navigationInfo { get; set; }
        public AppBarParams appBarParams { get; set; }
        public ClientPersistedCacheKey clientPersistedCacheKey { get; set; }
        public bool guestsEnabled { get; set; }
        public MenuData MenuData { get; set; }
        public int RecycleBinItemCount { get; set; }
        public int listItemCount { get; set; }
        public bool PublishingFeatureOn { get; set; }
        public bool PreviewFeaturesEnabled { get; set; }
        public bool disableAppViews { get; set; }
        public bool disableFlows { get; set; }
        public object serverRedirectedUrl { get; set; }
        public string formDigestValue { get; set; }
        public bool IsHomepageModernized { get; set; }
        public bool NextStepsFirstRunEnabled { get; set; }
        public int maximumFileSize { get; set; }
        public int formDigestTimeoutSeconds { get; set; }
        public bool canUserCreateMicrosoftForm { get; set; }
        public bool canUserCreateVisioDrawing { get; set; }
        public bool canUserUseSyntex { get; set; }
        public bool isSyntexEnabled { get; set; }
        public bool isSyntexExperienceEabled { get; set; }
        public bool isSyntexExperienceEnabled { get; set; }
        public bool isSyntexAIBuilderEabled { get; set; }
        public string syntexAIBuilderEnvironment { get; set; }
        public object readOnlyState { get; set; }
        public int resourceQuotaExceeded { get; set; }
        public bool preferUserTimeZone { get; set; }
        public object userTimeZoneData { get; set; }
        public int CalendarType { get; set; }
        public bool userTime24 { get; set; }
        public object userFirstDayOfWeek { get; set; }
        public WebTimeZoneData webTimeZoneData { get; set; }
        public bool webTime24 { get; set; }
        public int webFirstDayOfWeek { get; set; }
        public string aadTenantId { get; set; }
        public string aadUserId { get; set; }
        public string aadInstanceUrl { get; set; }
        public string aadSessionId { get; set; }
        public string msGraphEndpointUrl { get; set; }
        public string substrateEndpointUrl { get; set; }
        public string msMruEndpointUrl { get; set; }
        public bool allowInfectedDownload { get; set; }
        public List<OrganizationNewsSiteReference> organizationNewsSiteReference { get; set; }
        public CompanyPortalReference companyPortalReference { get; set; }
        public bool isCurrentSiteAHomeSite { get; set; }
        public bool isHomeSiteEnabled { get; set; }
        public object knowledgeHubSiteDetails { get; set; }
        public string spfx3rdPartyServicePrincipalId { get; set; }
        public object completenessUrls { get; set; }
        public bool socialBarEnabled { get; set; }
        public bool substrateOneDriveDisabled { get; set; }
        public bool isGroupifyDisabled { get; set; }
        public bool isGroupifyMenuButtonFeatureOff { get; set; }
        public bool Has2019Era { get; set; }
        public bool userVoiceForFeedbackEnabled { get; set; }
        public bool substrateOneDriveMigrated { get; set; }
        public string userPrincipalName { get; set; }
        public bool AddToOneDriveDisabled { get; set; }
        public bool spfxOBOFlowEnabled { get; set; }
        public string publicCdnBaseUrl { get; set; }
        public string userPhotoCdnBaseUrl { get; set; }
        public string MediaTAServiceHostUrl { get; set; }
        public string SideBySideToken { get; set; }
        public bool enforceIBSegmentFilter { get; set; }
        public bool disablePersonalListCreation { get; set; }
        public bool CommentsOnFilesDisabled { get; set; }
        public bool CommentsOnListItemsDisabled { get; set; }
        public bool DisableSpacesActivation { get; set; }
        public bool HideSyncButtonOnDocLib { get; set; }
        public bool IsAllowNullContext { get; set; }
        public FarmSettings farmSettings { get; set; }
        public bool PageParentsCommentsDisabled { get; set; }
        public bool HasSiteUsersExpiringSoon { get; set; }
        public object CurrentUserExpiration { get; set; }
        public bool listIsDefaultDocumentLibrary { get; set; }
        public bool viewInFileExplorer { get; set; }
        public bool isLabelIRMEnabled { get; set; }
    }

    internal class SpPages
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class SpPagesPreloads
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class SpPeopleWebpartsBundle
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class SpQuickLinksWebpart
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class SpRecommendeditemsWebpart
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class SpSafehtml
    {
        public string type { get; set; }
        public string path { get; set; }
    }

    internal class SpSearch
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class SpSearchCommon
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class SpSiteactivityWebpart
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class SpTelemetry
    {
        public string type { get; set; }
        public string path { get; set; }
    }

    internal class SpTitleRegionWebpart
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class SpToolbox
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class SpTopicShared
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class StandardDate
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int DayOfWeek { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }
        public int Milliseconds { get; set; }
    }

    internal class Strings
    {
        public string name { get; set; }
        public string nav { get; set; }
        public string home { get; set; }
        public string sites { get; set; }
        public string news { get; set; }
        public string files { get; set; }
        public string close { get; set; }
        public string create { get; set; }
        public string lists { get; set; }
    }

    internal class TeamsAce
    {
        public string type { get; set; }
        public string defaultPath { get; set; }
    }

    internal class ThemedImageFileNames
    {
        [JsonPropertyName("spcommon.png")]
        public string SpcommonPng { get; set; }

        [JsonPropertyName("ellipsis.11x11x32.png")]
        public string Ellipsis11x11x32Png { get; set; }

        [JsonPropertyName("O365BrandSuite.95x30x32.png")]
        public string O365BrandSuite95x30x32Png { get; set; }

        [JsonPropertyName("socialcommon.png")]
        public string SocialcommonPng { get; set; }

        [JsonPropertyName("spnav.png")]
        public string SpnavPng { get; set; }
    }

    internal class Title
    {
        public string @default { get; set; }
        public bool spfxSearchablePlainText { get; set; }
    }

    internal class Tslib
    {
        public string type { get; set; }
        public string id { get; set; }
        public string version { get; set; }
    }

    internal class Tslib2Bundle
    {
        public string type { get; set; }
        public string path { get; set; }
    }

    internal class UniqueId
    {
        public SpfxDependency spfxDependency { get; set; }
    }

    internal class Url
    {
        public SpfxDependency spfxDependency { get; set; }
        public bool spfxLink { get; set; }
    }

    internal class Url2
    {
        public SpfxDependency spfxDependency { get; set; }
        public bool spfxImageSource { get; set; }
        public bool spfxLink { get; set; }
    }

    internal class InternalUser
    {
        [JsonPropertyName("@odata.context")]
        public string OdataContext { get; set; }

        [JsonPropertyName("@odata.type")]
        public string OdataType { get; set; }

        [JsonPropertyName("@odata.id")]
        public string OdataId { get; set; }

        [JsonPropertyName("@odata.editLink")]
        public string OdataEditLink { get; set; }
        public int Id { get; set; }
        public bool IsHiddenInUI { get; set; }
        public string LoginName { get; set; }
        public string Title { get; set; }
        public int PrincipalType { get; set; }
        public string Email { get; set; }
        public string Expiration { get; set; }
        public bool IsEmailAuthenticationGuestUser { get; set; }
        public bool IsShareByEmailGuestUser { get; set; }
        public bool IsSiteAdmin { get; set; }
        public UserId UserId { get; set; }
        public string UserPrincipalName { get; set; }
    }

    internal class UserId
    {
        public string NameId { get; set; }
        public string NameIdIssuer { get; set; }
    }

    internal class UserInfo
    {
        public string _AuthorByline { get; set; }
        public object CheckoutUser { get; set; }
    }

    internal class VanityUrls
    {
    }

    internal class Viewers
    {
        public int Version { get; set; }
        public bool Enabled { get; set; }
    }

    internal class WebId
    {
        public SpfxDependency spfxDependency { get; set; }
    }

    internal class WebId2
    {
        public SpfxDependency spfxDependency { get; set; }
    }

    internal class WebPermMasks
    {
        public int High { get; set; }
        public int Low { get; set; }
    }

    internal class WebRelativeListUrl
    {
        public SpfxDependency spfxDependency { get; set; }
    }

    internal class WebTimeZoneData
    {
        public string Description { get; set; }
        public int Bias { get; set; }
        public int Id { get; set; }
        public int DaylightBias { get; set; }
        public DaylightDate DaylightDate { get; set; }
        public int StandardBias { get; set; }
        public StandardDate StandardDate { get; set; }
    }


}
