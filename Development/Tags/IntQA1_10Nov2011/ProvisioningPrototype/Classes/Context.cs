using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ProvisioningPrototype
{
    public class Context
    {
        public string ContextPageSource { get; set; }
        public List<ContextItem> ContextItems { get; set; }
        public int ContextIndex { get; set; }

        public Context(string parsedPageSource, int contextIndex)
        {
            ContextIndex = contextIndex;

            ContextPageSource = parsedPageSource;

            ContextItems = new List<ContextItem>();

            string communicationDashboardSource = ParseCommunicationSource(ContextPageSource);
            string contactInfoSource = ParseContactSource(ContextPageSource);
            string generalSource = ParseGeneralSource(ContextPageSource);
            string mediaSource = ParseMediaSource(ContextPageSource);
            string pathsSource = ParsePathsSource(ContextPageSource);
            string pointsManagement = ParsePointsSource(ContextPageSource);
            string portalSource = ParsePortalSource(ContextPageSource);
            string securitySource = ParseSecuritySource(ContextPageSource);

            var communication = new ContextItem(communicationDashboardSource, "Communication Dashboard");
            var contact = new ContextItem(contactInfoSource, "Contact Information");
            var general = new ContextItem(generalSource, "General");
            var media = new ContextItem(mediaSource, "Media");
            var paths = new ContextItem(pathsSource, "Paths");
            var points = new ContextItem(pointsManagement, "Points Management");
            var portal = new ContextItem(portalSource, "Portal");
            var security = new ContextItem(securitySource, "Security Requirements");

            ContextItems.Add(communication);
            ContextItems.Add(contact);
            ContextItems.Add(general);
            ContextItems.Add(media);
            ContextItems.Add(paths);
            ContextItems.Add(points);
            ContextItems.Add(portal);
            ContextItems.Add(security);

        }

        private static string ParseCommunicationSource(string contextPageSource)
        {
            var communicationSourceRegex = new Regex(@"\[\+\] Communication Dashboard(.*\[\+\] Contact Information)", RegexOptions.Singleline);
            Match match = communicationSourceRegex.Match(contextPageSource);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            throw new Exception("Regex was unable to find Communication Dashboard");
        }

        private static string ParseContactSource(string contextPageSource)
        {
            var contactSourceRegex = new Regex(@"\[\+\] Contact Information(.*\[\+\] General)", RegexOptions.Singleline);
            Match match = contactSourceRegex.Match(contextPageSource);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            throw new Exception("Regex was unable to find Contact Info");
        }

        private static string ParseGeneralSource(string contextPageSource)
        {
            var generalSourceRegex = new Regex(@"\[\+\] General(.*\[\+\] Media)", RegexOptions.Singleline);
            Match match = generalSourceRegex.Match(contextPageSource);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            throw new Exception("Regex was unable to find General");
        }

        private static string ParseMediaSource(string contextPageSource)
        {
            var mediaSourceRegex = new Regex(@"\[\+\] Media(.*\[\+\] Paths)", RegexOptions.Singleline);
            Match match = mediaSourceRegex.Match(contextPageSource);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            throw new Exception("Regex was unable to find Media");
        }

        private static string ParsePathsSource(string contextPageSource)
        {
            var pathsSourceRegex = new Regex(@"\[\+\] Paths(.*\[\+\] Points Management)", RegexOptions.Singleline);
            Match match = pathsSourceRegex.Match(contextPageSource);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            throw new Exception("Regex was unable to find Paths");
        }


        private static string ParsePointsSource(string contextPageSource)
        {
            var pointsSourceRegex = new Regex(@"\[\+\] Points Management(.*\[\+\] Portal)", RegexOptions.Singleline);
            Match match = pointsSourceRegex.Match(contextPageSource);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            throw new Exception("Regex was unable to find Points Management");
        }

        private static string ParsePortalSource(string contextPageSource)
        {
            var portalSourceRegex = new Regex(@"\[\+\] Portal(.*\[\+\] Security Requirements)", RegexOptions.Singleline);
            Match match = portalSourceRegex.Match(contextPageSource);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            throw new Exception("Regex was unable to find Portal");
        }

        private static string ParseSecuritySource(string contextPageSource)
        {
            var securitySourceRegex = new Regex(@"\[\+\] Security Requirements(.*)", RegexOptions.Singleline);
            Match match = securitySourceRegex.Match(contextPageSource);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            throw new Exception("Regex was unable to find Security Requirements");
        }
    }
}