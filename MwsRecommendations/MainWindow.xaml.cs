using MWSRecommendationsSectionService;
using MWSRecommendationsSectionService.Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;

namespace MwsRecommendations
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Get Last Update Recommendations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetLastUpdatedTimeForRecommendations_Click(object sender, RoutedEventArgs e)
        {
            string SellerId = CommonValue.strMerchantId;
            string MarketplaceId = CommonValue.strMarketplaceId;
            string AccessKeyId = CommonValue.strAccessKeyId;
            string SecretKeyId = CommonValue.strSecretKeyId;
            string ApplicationVersion = CommonValue.strApplicationVersion;
            string ApplicationName = CommonValue.strApplicationName;
            string MWSAuthToken = CommonValue.strMWSAuthToken;
            string strbuff = string.Empty;


            MWSRecommendationsSectionServiceConfig config = new MWSRecommendationsSectionServiceConfig();
            config.ServiceURL = CommonValue.strServiceURL;

            MWSRecommendationsSectionServiceClient client = new MWSRecommendationsSectionServiceClient(
                                                             AccessKeyId,
                                                             SecretKeyId,
                                                             ApplicationName,
                                                             ApplicationVersion,
                                                             config);
            GetLastUpdatedTimeForRecommendationsRequest request = new GetLastUpdatedTimeForRecommendationsRequest();
            request.SellerId = SellerId;
            request.MarketplaceId = MarketplaceId;
            request.MWSAuthToken = MWSAuthToken;
            GetLastUpdatedTimeForRecommendationsResponse response = client.GetLastUpdatedTimeForRecommendations(request);
            if (response.IsSetGetLastUpdatedTimeForRecommendationsResult())
            {
                strbuff += "在庫情報更新日付：" + response.GetLastUpdatedTimeForRecommendationsResult.InventoryRecommendationsLastUpdated.Date.ToString() + System.Environment.NewLine;
                strbuff += "セレクション情報更新日付：" + response.GetLastUpdatedTimeForRecommendationsResult.SelectionRecommendationsLastUpdated.Date.ToString() + System.Environment.NewLine;
                strbuff += "価格情報更新日付：" + response.GetLastUpdatedTimeForRecommendationsResult.PricingRecommendationsLastUpdated.Date.ToString() + System.Environment.NewLine;
                strbuff += "広告情報更新日付：" + response.GetLastUpdatedTimeForRecommendationsResult.AdvertisingRecommendationsLastUpdated.Date.ToString() + System.Environment.NewLine;
                strbuff += "海外出品情報更新日付：" + response.GetLastUpdatedTimeForRecommendationsResult.GlobalSellingRecommendationsLastUpdated.Date.ToString() + System.Environment.NewLine;
            }
            txtGetLastUpdatedTimeForRecommendations.Text = strbuff;
        }

        /// <summary>
        /// Get Recommendations List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnListRecommendations_Click(object sender, RoutedEventArgs e)
        {
            string SellerId = CommonValue.strMerchantId;
            string MarketplaceId = CommonValue.strMarketplaceId;
            string AccessKeyId = CommonValue.strAccessKeyId;
            string SecretKeyId = CommonValue.strSecretKeyId;
            string ApplicationVersion = CommonValue.strApplicationVersion;
            string ApplicationName = CommonValue.strApplicationName;
            string MWSAuthToken = CommonValue.strMWSAuthToken;
            string strbuff = string.Empty;


            MWSRecommendationsSectionServiceConfig config = new MWSRecommendationsSectionServiceConfig();
            config.ServiceURL = CommonValue.strServiceURL;

            MWSRecommendationsSectionServiceClient client = new MWSRecommendationsSectionServiceClient(
                                                             AccessKeyId,
                                                             SecretKeyId,
                                                             ApplicationName,
                                                             ApplicationVersion,
                                                             config);
            ListRecommendationsRequest request = new ListRecommendationsRequest();
            request.SellerId = SellerId;
            request.MarketplaceId = MarketplaceId;
            request.MWSAuthToken = MWSAuthToken;
            request.RecommendationCategory = "Inventory";

            // 処理実行
            ListRecommendationsResponse response = client.ListRecommendations(request);
            if (response.IsSetListRecommendationsResult())
            {
                List<InventoryRecommendation> inventoryRecommendations = response.ListRecommendationsResult.InventoryRecommendations;
                foreach (InventoryRecommendation inventoryRecommendation in inventoryRecommendations)
                {
                    strbuff += "商品名：" + inventoryRecommendation.ItemName + " 更新日：" + inventoryRecommendation.LastUpdated.Date.ToString() + System.Environment.NewLine;
                }
            }
            txtListRecommendations.Text = strbuff;
        }

        /// <summary>
        /// Get Next Recommendations List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLIstRecommendationsByNextToken_Click(object sender, RoutedEventArgs e)
        {
            string SellerId = CommonValue.strMerchantId;
            string MarketplaceId = CommonValue.strMarketplaceId;
            string AccessKeyId = CommonValue.strAccessKeyId;
            string SecretKeyId = CommonValue.strSecretKeyId;
            string ApplicationVersion = CommonValue.strApplicationVersion;
            string ApplicationName = CommonValue.strApplicationName;
            string MWSAuthToken = CommonValue.strMWSAuthToken;
            string strbuff = string.Empty;


            MWSRecommendationsSectionServiceConfig config = new MWSRecommendationsSectionServiceConfig();
            config.ServiceURL = CommonValue.strServiceURL;

            MWSRecommendationsSectionServiceClient client = new MWSRecommendationsSectionServiceClient(
                                                             AccessKeyId,
                                                             SecretKeyId,
                                                             ApplicationName,
                                                             ApplicationVersion,
                                                             config);
            ListRecommendationsRequest request = new ListRecommendationsRequest();
            request.SellerId = SellerId;
            request.MarketplaceId = MarketplaceId;
            request.MWSAuthToken = MWSAuthToken;
            request.RecommendationCategory = "Inventory";

            ListRecommendationsResponse response = client.ListRecommendations(request);
            if (response.IsSetListRecommendationsResult())
            {
                if (response.ListRecommendationsResult.NextToken != null)
                {
                    ListRecommendationsByNextTokenRequest request1 = new ListRecommendationsByNextTokenRequest();
                    request1.SellerId = SellerId;
                    request1.NextToken = response.ListRecommendationsResult.NextToken;

                    ListRecommendationsByNextTokenResponse response1 = client.ListRecommendationsByNextToken(request1);
                    if (response1.IsSetListRecommendationsByNextTokenResult())
                    {
                        List<InventoryRecommendation> inventoryRecommendations = response.ListRecommendationsResult.InventoryRecommendations;
                        foreach (InventoryRecommendation inventoryRecommendation in inventoryRecommendations)
                        {
                            strbuff += "商品名：" + inventoryRecommendation.ItemName + " 更新日：" + inventoryRecommendation.LastUpdated.Date.ToString() + System.Environment.NewLine;
                        }
                    }
                }
            }
            txtListRecommendationsByNextToken.Text = strbuff;
        }

        /// <summary>
        /// Get Service Status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetServiceStatus_Click(object sender, RoutedEventArgs e)
        {
            string SellerId = CommonValue.strMerchantId;
            string MarketplaceId = CommonValue.strMarketplaceId;
            string AccessKeyId = CommonValue.strAccessKeyId;
            string SecretKeyId = CommonValue.strSecretKeyId;
            string ApplicationVersion = CommonValue.strApplicationVersion;
            string ApplicationName = CommonValue.strApplicationName;
            string MWSAuthToken = CommonValue.strMWSAuthToken;
            string strbuff = string.Empty;


            MWSRecommendationsSectionServiceConfig config = new MWSRecommendationsSectionServiceConfig();
            config.ServiceURL = CommonValue.strServiceURL;

            MWSRecommendationsSectionServiceClient client = new MWSRecommendationsSectionServiceClient(
                                                             AccessKeyId,
                                                             SecretKeyId,
                                                             ApplicationName,
                                                             ApplicationVersion,
                                                             config);
            GetServiceStatusRequest request = new GetServiceStatusRequest();
            request.SellerId = SellerId;
            request.MWSAuthToken = MWSAuthToken;

            GetServiceStatusResponse response = client.GetServiceStatus(request);
            if (response.IsSetGetServiceStatusResult())
            {
                strbuff = "サービス状態：" + response.GetServiceStatusResult.Status;
            }
            txtGetServiceStatus.Text = strbuff;
        }
    }
}
