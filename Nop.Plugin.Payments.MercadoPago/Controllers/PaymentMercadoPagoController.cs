using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Plugin.Payments.MercadoPago.Models;
using Nop.Services;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Orders;
using Nop.Services.Payments;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Payments.MercadoPago.Controllers
{
    public class PaymentMercadoPagoController : BasePaymentController
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly ILogger _logger;
        private readonly IOrderProcessingService _orderProcessingService;
        private readonly IOrderService _orderService;
        private readonly IPermissionService _permissionService;
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;
        private readonly IStoreService _storeService;
        private readonly IWorkContext _workContext;
        private readonly IWebHelper _webHelper;

        #endregion

        #region Ctor

        public PaymentMercadoPagoController(ILocalizationService localizationService,
            ILogger logger,
            IOrderProcessingService orderProcessingService,
            IOrderService orderService,
            IPermissionService permissionService,
            ISettingService settingService,
            IStoreContext storeContext,
            IStoreService storeService,
            IWorkContext workContext,
            IWebHelper webHelper)
        {
            this._localizationService = localizationService;
            this._logger = logger;
            this._orderProcessingService = orderProcessingService;
            this._orderService = orderService;
            this._permissionService = permissionService;
            this._settingService = settingService;
            this._storeContext = storeContext;
            this._storeService = storeService;
            this._workContext = workContext;
            this._webHelper = webHelper;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Create webhook that receive events for the subscribed event types
        /// </summary>
        /// <returns>Webhook id</returns>
        protected string CreateWebHook()
        {
            var storeScope = GetActiveStoreScopeConfiguration(_storeService, _workContext);
            //var payPalDirectPaymentSettings = _settingService.LoadSetting<PayPalDirectPaymentSettings>(storeScope);

            return string.Empty;
        }

        #endregion

        #region Methods

        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public IActionResult Configure()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManagePaymentMethods))
                return AccessDeniedView();

            //load settings for a chosen store scope
            var storeScope = GetActiveStoreScopeConfiguration(_storeService, _workContext);
            //var payPalDirectPaymentSettings = _settingService.LoadSetting<PayPalDirectPaymentSettings>(storeScope);

            var model = new ConfigurationModel();
            //if (storeScope > 0)
            //{
            //    model.ClientId_OverrideForStore = _settingService.SettingExists(payPalDirectPaymentSettings, x => x.ClientId, storeScope);
            //    model.ClientSecret_OverrideForStore = _settingService.SettingExists(payPalDirectPaymentSettings, x => x.ClientSecret, storeScope);
            //    model.UseSandbox_OverrideForStore = _settingService.SettingExists(payPalDirectPaymentSettings, x => x.UseSandbox, storeScope);
            //    model.PassPurchasedItems_OverrideForStore = _settingService.SettingExists(payPalDirectPaymentSettings, x => x.PassPurchasedItems, storeScope);
            //    model.TransactModeId_OverrideForStore = _settingService.SettingExists(payPalDirectPaymentSettings, x => x.TransactMode, storeScope);
            //    model.AdditionalFee_OverrideForStore = _settingService.SettingExists(payPalDirectPaymentSettings, x => x.AdditionalFee, storeScope);
            //    model.AdditionalFeePercentage_OverrideForStore = _settingService.SettingExists(payPalDirectPaymentSettings, x => x.AdditionalFeePercentage, storeScope);
            //}

            return View("~/Plugins/Payments.MercadoPago/Views/Configure.cshtml", model);
        }

        [HttpPost, ActionName("Configure")]
        [FormValueRequired("save")]
        [AuthorizeAdmin]
        [AdminAntiForgery]
        [Area(AreaNames.Admin)]
        public IActionResult Configure(ConfigurationModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManagePaymentMethods))
                return AccessDeniedView();

            if (!ModelState.IsValid)
                return Configure();

            //load settings for a chosen store scope
            var storeScope = GetActiveStoreScopeConfiguration(_storeService, _workContext);
            dynamic payPalDirectPaymentSettings; // new PayPalDirectPaymentSettings();// _settingService.LoadSetting<PayPalDirectPaymentSettings>(storeScope);

            //save settings
            //payPalDirectPaymentSettings.ClientId = model.ClientId;
            //payPalDirectPaymentSettings.ClientSecret = model.ClientSecret;
            //payPalDirectPaymentSettings.WebhookId = model.WebhookId;
            //payPalDirectPaymentSettings.UseSandbox = model.UseSandbox;
            //payPalDirectPaymentSettings.PassPurchasedItems = model.PassPurchasedItems;
            //payPalDirectPaymentSettings.TransactMode = (TransactMode)model.TransactModeId;
            //payPalDirectPaymentSettings.AdditionalFee = model.AdditionalFee;
            //payPalDirectPaymentSettings.AdditionalFeePercentage = model.AdditionalFeePercentage;

            /* We do not clear cache after each setting update.
             * This behavior can increase performance because cached settings will not be cleared 
             * and loaded from database after each update */
            //_settingService.SaveSettingOverridablePerStore(payPalDirectPaymentSettings, x => x.ClientId, model.ClientId_OverrideForStore, storeScope, false);
            //_settingService.SaveSettingOverridablePerStore(payPalDirectPaymentSettings, x => x.ClientSecret, model.ClientSecret_OverrideForStore, storeScope, false);
            //_settingService.SaveSetting(payPalDirectPaymentSettings, x => x.WebhookId, 0, false);
            //_settingService.SaveSettingOverridablePerStore(payPalDirectPaymentSettings, x => x.UseSandbox, model.UseSandbox_OverrideForStore, storeScope, false);
            //_settingService.SaveSettingOverridablePerStore(payPalDirectPaymentSettings, x => x.PassPurchasedItems, model.PassPurchasedItems_OverrideForStore, storeScope, false);
            //_settingService.SaveSettingOverridablePerStore(payPalDirectPaymentSettings, x => x.TransactMode, model.TransactModeId_OverrideForStore, storeScope, false);
            //_settingService.SaveSettingOverridablePerStore(payPalDirectPaymentSettings, x => x.AdditionalFee, model.AdditionalFee_OverrideForStore, storeScope, false);
            //_settingService.SaveSettingOverridablePerStore(payPalDirectPaymentSettings, x => x.AdditionalFeePercentage, model.AdditionalFeePercentage_OverrideForStore, storeScope, false);

            //now clear settings cache
            _settingService.ClearCache();

            SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

            return Configure();
        }

        [HttpPost, ActionName("Configure")]
        [FormValueRequired("createwebhook")]
        [AuthorizeAdmin]
        [AdminAntiForgery]
        [Area(AreaNames.Admin)]
        public IActionResult GetWebhookId(ConfigurationModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManagePaymentMethods))
                return AccessDeniedView();

            //var payPalDirectPaymentSettings = _settingService.LoadSetting<PayPalDirectPaymentSettings>();
            //payPalDirectPaymentSettings.WebhookId = CreateWebHook();
            //_settingService.SaveSetting(payPalDirectPaymentSettings);
            //
            //if (string.IsNullOrEmpty(payPalDirectPaymentSettings.WebhookId))
            //    ErrorNotification(_localizationService.GetResource("Plugins.Payments.PayPalDirect.WebhookError"));

            return Configure();
        }

        [HttpPost]
        public IActionResult WebhookEventsHandler()
        {
            var storeScope = GetActiveStoreScopeConfiguration(_storeService, _workContext);
            //var payPalDirectPaymentSettings = _settingService.LoadSetting<PayPalDirectPaymentSettings>(storeScope);

            try
            {
                var requestBody = string.Empty;
                using (var stream = new StreamReader(this.Request.Body, Encoding.UTF8))
                {
                    requestBody = stream.ReadToEnd();
                }


                return Ok();
            }
            catch (Exception ex)
            {
                return Ok();
            }
        }

        #endregion
    }
}